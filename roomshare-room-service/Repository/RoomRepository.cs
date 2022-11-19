using AutoMapper;
using roomshare_room_service.Data.ValueObjects;
using roomshare_room_service.Model;
using roomshare_room_service.Model.Context;

namespace roomshare_room_service.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly MySQLContext _context;
        private IMapper _mapper;

        public RoomRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomVO> Create(RoomVO vo)
        {
            Room room = _mapper.Map<Room>(vo);

            _context.Rooms.Add(room);

            await _context.SaveChangesAsync();

            return _mapper.Map<RoomVO>(room);
        }
    }
}
