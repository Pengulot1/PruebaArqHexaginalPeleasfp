using PeleasReprobadosfp.domain.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.application.Application
{
    public record DeleteFighterCommand(int Id);

    public class DeleteFighterCommandHandler
    {
        private readonly IFighterRepository _repository;

        public DeleteFighterCommandHandler(IFighterRepository repository) => _repository = repository;

        public async Task Handle(DeleteFighterCommand command)
        {
            var fighter = await _repository.GetByIdAsync(command.Id);
            if (fighter == null) throw new Exception("Peleador no encontrado");

            await _repository.DeleteAsync(fighter);
        }
    }
    
}
