using PeleasReprobadosfp.domain.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.application.Application.Weapons
{
    public record GetWeaponByIdQuery(int Id);

    public class GetWeaponByIdQueryHandler
    {
        private readonly IWeaponRepository _repository;

        public GetWeaponByIdQueryHandler(IWeaponRepository repository) => _repository = repository;

        public async Task<WeaponDto?> Handle(GetWeaponByIdQuery query)
        {
            var weapon = await _repository.GetByIdAsync(query.Id);
            if (weapon == null) return null;

            return new WeaponDto(weapon.Id, weapon.Name, weapon.Damage, weapon.Durability);
        }
    }

    // 2. Obtener Todas las Armas
    public record GetAllWeaponsQuery();

    public class GetAllWeaponsQueryHandler
    {
        private readonly IWeaponRepository _repository;

        public GetAllWeaponsQueryHandler(IWeaponRepository repository) => _repository = repository;

        public async Task<IEnumerable<WeaponDto>> Handle(GetAllWeaponsQuery query)
        {
            var weapons = await _repository.GetAllAsync();
            return weapons.Select(w => new WeaponDto(w.Id, w.Name, w.Damage, w.Durability));
        }
    }
}
