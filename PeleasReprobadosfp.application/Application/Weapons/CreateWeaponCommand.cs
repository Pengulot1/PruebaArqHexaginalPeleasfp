using PeleasReprobadosfp.domain.Domain;
using PeleasReprobadosfp.domain.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.application.Application.Weapons
{
    public record CreateWeaponCommand(int Id, string Name, int Damage, int Durability);

    public class CreateWeaponCommandHandler
    {
        private readonly IWeaponRepository _repository;

        public CreateWeaponCommandHandler(IWeaponRepository repository) => _repository = repository;

        public async Task Handle(CreateWeaponCommand command)
        {
            var weapon = new Weapon(command.Id, command.Name, command.Damage, command.Durability);
            await _repository.AddAsync(weapon);
        }
    }
}
