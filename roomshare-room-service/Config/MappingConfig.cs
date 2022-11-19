using AutoMapper;
using roomshare_room_service.Data.ValueObjects;
using roomshare_room_service.Model;

namespace roomshare_room_service.Config
{
    public class MappingConfig {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<RoomVO, Room>();
                config.CreateMap<Room, RoomVO>();
            });

            return mappingConfig;            
        }
    }
}
