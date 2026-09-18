using Microsoft.AspNetCore.Mvc;
using PeleasReprobadosfp.application.Application;
using PeleasReprobadosfp.application.Application.Fighters;

namespace PeleasReprobadosfp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FighterController : ControllerBase
    {
        private readonly CreateFighterCommandHandler _createHandler;
        private readonly UpdateFighterCommandHandler _updateHandler;
        private readonly DeleteFighterCommandHandler _deleteHandler;
        private readonly GetFighterByIdQueryHandler _getByIdHandler;
        private readonly GetAllFightersQueryHandler _getAllHandler;

        public FighterController(
            CreateFighterCommandHandler createHandler,
            UpdateFighterCommandHandler updateHandler,
            DeleteFighterCommandHandler deleteHandler,
            GetFighterByIdQueryHandler getByIdHandler,
            GetAllFightersQueryHandler getAllHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getByIdHandler = getByIdHandler;
            _getAllHandler = getAllHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFighterCommand command)
        {
            await _createHandler.Handle(command);
            return Ok("Peleador registrado correctamente.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllHandler.Handle(new GetAllFightersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(new GetFighterByIdQuery(id));
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFighterCommand command)
        {
            await _updateHandler.Handle(command);
            return Ok("Peleador actualizado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _deleteHandler.Handle(new DeleteFighterCommand(id));
            return Ok("Peleador eliminado.");
        }
    }
}
