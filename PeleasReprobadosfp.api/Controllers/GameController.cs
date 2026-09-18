using Microsoft.AspNetCore.Mvc;
using PeleasReprobadosfp.application.Application.Fighters;




namespace PeleasReprobadosfp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly EquipWeaponCommandHandler _equipHandler;
        private readonly SimulateCombatCommandHandler _combatHandler;

        public GameController(
            EquipWeaponCommandHandler equipHandler,
            SimulateCombatCommandHandler combatHandler)
        {
            _equipHandler = equipHandler;
            _combatHandler = combatHandler;
        }

        // ENDPOINT 1: Equipar Arma
        // POST: api/game/equip-weapon
        [HttpPost("equip-weapon")]
        public async Task<IActionResult> EquipWeapon([FromBody] EquipWeaponCommand command)
        {
            try
            {
                await _equipHandler.Handle(command);
                return Ok("Arma equipada exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ENDPOINT 2: Simular Combate
        // POST: api/game/simulate-combat
        [HttpPost("simulate-combat")]
        public async Task<IActionResult> SimulateCombat([FromBody] SimulateCombatCommand command)
        {
            try
            {
                var result = await _combatHandler.Handle(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
