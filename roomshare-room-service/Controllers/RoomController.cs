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

        /// <summary>
        /// Cadastrar uma sala comercial.
        /// </summary>
        /// <remarks>
        /// Requisicão simples (é preciso passar o Token de autorização):
        /// 
        ///     POST api/v1/Room
        ///     {
        ///         "name": "Sala Comercial em Moema",
        ///         "description": "Bela sala comercial 50m2,ar.cond,uma vaga a 2 quadra do metrô Moema, restaurantes e bares na porta",  
        ///         "address": "Avenida Moema, 500",
        ///         "CEP": 12345678
        ///     }
        /// </remarks>
        /// <param name="room"></param>    
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

        /// <summary>
        /// Atualizar cadastro de uma sala comercial.
        /// </summary>
        /// <remarks>
        /// Requisicão simples (é preciso passar o Token de autorização):
        /// 
        ///     PUT api/v1/Room
        ///     {
        ///         "id": 90,
        ///         "name": "Sala Comercial em Moema",
        ///         "description": "Bela sala comercial 50m2,ar.cond,uma vaga a 2 quadra do metrô Moema, restaurantes e bares na porta",  
        ///         "address": "Avenida Moema, 500",
        ///         "CEP": 12345678
        ///     }
        /// </remarks>
        /// <param name="room"></param>   
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

        /// <summary>
        /// Excluir cadastro de uma sala comercial.
        /// </summary>
        /// <param name="Id"></param>  
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
