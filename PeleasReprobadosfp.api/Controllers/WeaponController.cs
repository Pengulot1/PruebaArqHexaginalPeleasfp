using Microsoft.AspNetCore.Mvc;
using PeleasReprobadosfp.application.Application.Weapons;

namespace PeleasReprobadosfp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly CreateWeaponCommandHandler _createHandler;
        private readonly UpdateWeaponCommandHandler _updateHandler;
        private readonly DeleteWeaponCommandHandler _deleteHandler;
        private readonly GetAllWeaponsQueryHandler _getAllHandler;

        public WeaponController(
            CreateWeaponCommandHandler createHandler,
            UpdateWeaponCommandHandler updateHandler,
            DeleteWeaponCommandHandler deleteHandler,
            GetAllWeaponsQueryHandler getAllHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getAllHandler = getAllHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWeaponCommand command)
        {
            await _createHandler.Handle(command);
            return Ok("Arma creada correctamente.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllHandler.Handle(new GetAllWeaponsQuery());
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _deleteHandler.Handle(new DeleteWeaponCommand(id));
            return Ok("Arma eliminada.");
        }
    }
}
