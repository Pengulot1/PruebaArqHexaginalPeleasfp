using PeleasReprobadosfp.domain.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.application.Application.Fighters
{
    public record EquipWeaponCommand(int FighterId, int WeaponId);

    public class EquipWeaponCommandHandler
    {
        private readonly IFighterRepository _fighterRepo;
        private readonly IWeaponRepository _weaponRepo;

        public EquipWeaponCommandHandler(IFighterRepository fighterRepo, IWeaponRepository weaponRepo)
        {
            _fighterRepo = fighterRepo;
            _weaponRepo = weaponRepo;
        }

        public async Task Handle(EquipWeaponCommand command)
        {
            var fighter = await _fighterRepo.GetByIdAsync(command.FighterId);
            if (fighter == null) throw new Exception("Peleador no encontrado");

            var weapon = await _weaponRepo.GetByIdAsync(command.WeaponId);
            if (weapon == null) throw new Exception("Arma no encontrada");

            // Le asignamos el arma al peleador en el dominio
            fighter.EquipWeapon(weapon.Id);

            // Guardamos en la base de datos (Firestore)
            await _fighterRepo.UpdateAsync(fighter);
        }
    }
}

