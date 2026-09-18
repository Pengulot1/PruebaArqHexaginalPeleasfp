using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.domain.Domain.Interfaces
{
    public interface IFighterRepository
    {
        Task<Fighter?> GetByIdAsync(int id);
        Task<IEnumerable<Fighter>> GetAllAsync();
        Task AddAsync(Fighter fighter);
        Task UpdateAsync(Fighter fighter);
        Task DeleteAsync(Fighter fighter);
    }
}
