using PeleasReprobadosfp.domain.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.application.Application
{
    public record GetFighterByIdQuery(int Id);

    public class GetFighterByIdQueryHandler
    {
        private readonly IFighterRepository _repository;

        public GetFighterByIdQueryHandler(IFighterRepository repository) => _repository = repository;

        public async Task<FighterDto?> Handle(GetFighterByIdQuery query)
        {
            var fighter = await _repository.GetByIdAsync(query.Id);
            if (fighter == null) return null;

            // Mapeamos de Dominio a DTO
            return new FighterDto(fighter.Id, fighter.Name, fighter.Health, fighter.Strength, fighter.EquippedWeaponId);
        }
    }

    public record GetAllFightersQuery();

public class GetAllFightersQueryHandler 
{
    private readonly IFighterRepository _repository;

    public GetAllFightersQueryHandler(IFighterRepository repository) => _repository = repository;

    public async Task<IEnumerable<FighterDto>> Handle(GetAllFightersQuery query) 
    {
        var fighters = await _repository.GetAllAsync();
        return fighters.Select(f => new FighterDto(f.Id, f.Name, f.Health, f.Strength, f.EquippedWeaponId));
    }
}
}
