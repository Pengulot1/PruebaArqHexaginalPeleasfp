using PeleasReprobadosfp.domain.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.application.Application
{
    public record UpdateFighterCommand(int Id, string Name, int Health, int Strength);

    public class UpdateFighterCommandHandler
    {
        private readonly IFighterRepository _fighterRepository;

        public UpdateFighterCommandHandler(IFighterRepository fighterRepository)
        {
            _fighterRepository = fighterRepository;
        }

        public async Task Handle(UpdateFighterCommand command)
        {
            var fighter = await _fighterRepository.GetByIdAsync(command.Id);
            if (fighter == null) throw new Exception("Fighter not found"); // O una custom exception

            // Usamos el método de dominio, no modificamos propiedades directamente
            fighter.UpdateStats(command.Name, command.Health, command.Strength);

            await _fighterRepository.UpdateAsync(fighter);
        }
    }
}
