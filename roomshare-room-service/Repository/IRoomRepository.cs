using roomshare_room_service.Data.ValueObjects;
using roomshare_room_service.Model;

namespace roomshare_room_service.Repository
{
    public interface IRoomRepository
    {
        Task<RoomVO> Create(RoomVO room);
    }
}
