using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using roomshare_room_service.Data.ValueObjects;
using roomshare_room_service.Repository;
using roomshare_room_service_command.Integration;

namespace roomshare_room_service.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoomController: ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private IRoomRepository _roomService;

        public RoomController(ILogger<RoomController> logger, IRoomRepository roomService): base()
        {
            _logger = logger;
            _roomService = roomService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RoomVO room)
        {
            var token = Request.Headers["Authorization"];
            var user = await AuthIntegration.GetUser(token);

            if(user == null)
            {
                return Unauthorized("Usuário inválido, tente novamente.");
            }

            room.LocatorKey = new Guid(user.guid);

            return Ok(await _roomService.Create(room));
        }

        [HttpPut]
        public async Task<IActionResult> Put(RoomVO room)
        {
            var token = Request.Headers["Authorization"];
            var user = await AuthIntegration.GetUser(token);

            if (user == null)
            {
                return Unauthorized("Usuário inválido, tente novamente.");
            }

            room.LocatorKey = new Guid(user.guid);

            return Ok(await _roomService.Update(room));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long Id)
        {
            var token = Request.Headers["Authorization"];
            var user = await AuthIntegration.GetUser(token);

            if (user == null)
            {
                return Unauthorized("Usuário inválido, tente novamente.");
            }

            return Ok(await _roomService.Delete(Id));
        } 
    }
}
