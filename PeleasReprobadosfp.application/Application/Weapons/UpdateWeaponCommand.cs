using PeleasReprobadosfp.domain.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.application.Application.Weapons
{
    public record UpdateWeaponCommand(int Id, string Name, int Damage, int Durability);

    public class UpdateWeaponCommandHandler
    {
        private readonly IWeaponRepository _repository;

        public UpdateWeaponCommandHandler(IWeaponRepository repository) => _repository = repository;

        public async Task Handle(UpdateWeaponCommand command)
        {
            var weapon = await _repository.GetByIdAsync(command.Id);
            if (weapon == null) throw new Exception("no se encontro tu arma weon");

            weapon.UpdateStats(command.Name, command.Damage, command.Durability);
            await _repository.UpdateAsync(weapon);
        }
    }
}
