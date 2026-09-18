using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.domain.Domain.Interfaces
{
    public interface IWeaponRepository
    {
        Task<Weapon?> GetByIdAsync(int id);
        Task<IEnumerable<Weapon>> GetAllAsync();
        Task AddAsync(Weapon weapon);
        Task UpdateAsync(Weapon weapon);
        Task DeleteAsync(Weapon weapon);
    }
}
