using AutoMapper;
using Newtonsoft.Json;
using roomshare_room_service.Data.ValueObjects;
using roomshare_room_service.Model;
using roomshare_room_service.Model.Context;
using roomshare_room_service_command.Data.ValueObjects;
using roomshare_room_service_command.Service;

namespace roomshare_room_service.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;
        private IConfiguration _configuration;
        private string _host;
        private string _port;
        private string _topic;

        public RoomRepository(MySQLContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _host = _configuration["KAFKAHOST"];
            _port = _configuration["KAFKAPORT"];
            _topic = _configuration["KafkaTopics:RoomChange"];
        }

        public async Task<RoomVO> Create(RoomVO vo)
        {
            Room room = _mapper.Map<Room>(vo);

            _context.Rooms.Add(room);

            await _context.SaveChangesAsync();

            RoomKafkaVO roomKafkaVO = new RoomKafkaVO()
            {
                Method = "POST",
                Id = room.Id,
                Address = room.Address,
                CEP = room.CEP,
                Description = room.Description,
                LocatorKey = room.LocatorKey,
                Name = room.Name,
                RoomKey = room.RoomKey
            };

            KafkaProducerService.SendChangeRequest(_host, _port, _topic, JsonConvert.SerializeObject(roomKafkaVO));

            return _mapper.Map<RoomVO>(room);
        }

        public async Task<RoomVO> Update(RoomVO vo)
        {
            var room = _mapper.Map<Room>(vo);

            _context.Update(room);

            await _context.SaveChangesAsync();

            RoomKafkaVO roomKafkaVO = new RoomKafkaVO()
            {
                Method = "PUT",
                Id = room.Id,
                Address = room.Address,
                CEP = room.CEP,
                Description = room.Description,
                LocatorKey = room.LocatorKey,
                Name = room.Name,
                RoomKey = room.RoomKey
            };

            KafkaProducerService.SendChangeRequest(_host, _port, _topic, JsonConvert.SerializeObject(roomKafkaVO));

            return _mapper.Map<RoomVO>(room);
        }

        public async Task<bool> Delete(long Id)
        {
            var room = _context.Rooms.Where(x => x.Id == Id).First();

            _context.Rooms.Remove(room);

            await _context.SaveChangesAsync();

            RoomKafkaVO roomKafkaVO = new RoomKafkaVO()
            {
                Method = "DELETE",
                Id = room.Id,
                Address = room.Address,
                CEP = room.CEP,
                Description = room.Description,
                LocatorKey = room.LocatorKey,
                Name = room.Name,
                RoomKey = room.RoomKey
            };

            KafkaProducerService.SendChangeRequest(_host, _port, _topic, JsonConvert.SerializeObject(roomKafkaVO));

            return true;
        }
    }
}
