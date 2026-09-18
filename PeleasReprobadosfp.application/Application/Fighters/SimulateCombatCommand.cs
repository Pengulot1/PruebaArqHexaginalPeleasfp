using PeleasReprobadosfp.domain.Domain.Interfaces;
using PeleasReprobadosfp.domain.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.application.Application.Fighters
{
    public record SimulateCombatCommand(int Fighter1Id, int Fighter2Id);

    public class SimulateCombatCommandHandler
    {
        private readonly IFighterRepository _fighterRepo;
        private readonly IWeaponRepository _weaponRepo;
        private readonly CombatDomainService _combatService;

        public SimulateCombatCommandHandler(
            IFighterRepository fighterRepo,
            IWeaponRepository weaponRepo,
            CombatDomainService combatService)
        {
            _fighterRepo = fighterRepo;
            _weaponRepo = weaponRepo;
            _combatService = combatService;
        }

        public async Task<string> Handle(SimulateCombatCommand command)
        {
            // 1. Obtener peleadores
            var fighter1 = await _fighterRepo.GetByIdAsync(command.Fighter1Id) ?? throw new Exception("Peleador 1 no existe");
            var fighter2 = await _fighterRepo.GetByIdAsync(command.Fighter2Id) ?? throw new Exception("Peleador 2 no existe");

            // 2. Obtener sus armas (si las tienen)
            var weapon1 = fighter1.EquippedWeaponId.HasValue
                ? await _weaponRepo.GetByIdAsync(fighter1.EquippedWeaponId.Value)
                : null;

            var weapon2 = fighter2.EquippedWeaponId.HasValue
                ? await _weaponRepo.GetByIdAsync(fighter2.EquippedWeaponId.Value)
                : null;

            // 3. Simular combate
            return _combatService.SimulateFight(fighter1, weapon1, fighter2, weapon2);
        }
    }
}

