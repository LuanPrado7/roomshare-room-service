using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using roomshare_room_service.Data.ValueObjects;
using roomshare_room_service.Repository;

namespace roomshare_room_service.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private IRoomRepository _roomService;

        public RoomController(ILogger<RoomController> logger, IRoomRepository roomService)
        {
            _logger = logger;
            _roomService = roomService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RoomVO room)
        {          
            return Ok(await _roomService.Create(room));
        }
    }
}
