using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeleasReprobadosfp.domain.Domain;
using PeleasReprobadosfp.domain.Domain.Interfaces;

namespace PeleasReprobadosfp.application.Application
{
    public record CreateFighterCommand(int Id, string Name, int Health, int Strength);

    // 2. El Manejador (La orquestación)
    public class CreateFighterCommandHandler
    {
        private readonly IFighterRepository _fighterRepository;

        public CreateFighterCommandHandler(IFighterRepository fighterRepository)
        {
            _fighterRepository = fighterRepository;
        }

        public async Task Handle(CreateFighterCommand command)
        {
            // Se crea usando el constructor del dominio (nace sin arma)
            var newFighter = new Fighter(
                command.Id,
                command.Name,
                command.Health,
                command.Strength
            );

            await _fighterRepository.AddAsync(newFighter);
        }
    }

}
