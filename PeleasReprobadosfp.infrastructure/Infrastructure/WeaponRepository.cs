using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PeleasReprobadosfp.domain.Domain.Interfaces;
using PeleasReprobadosfp.domain.Domain;

namespace PeleasReprobadosfp.infrastructure.Infrastructure
{
    public class WeaponRepository : IWeaponRepository
    {
        private readonly GameDbContext _context;
        private readonly ILogger<WeaponRepository> _logger;

        public WeaponRepository(GameDbContext context, ILogger<WeaponRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Weapon?> GetByIdAsync(int id)
        {
            // FindAsync es el método más rápido en EF Core para buscar por Llave Primaria
            return await _context.Weapons.FindAsync(id);
        }

        public async Task<IEnumerable<Weapon>> GetAllAsync()
        {
            // AsNoTracking() es ideal para consultas de solo lectura, mejora el rendimiento
            return await _context.Weapons.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(Weapon weapon)
        {
            try
            {
                await _context.Weapons.AddAsync(weapon);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error de base de datos al intentar crear el arma con ID {WeaponId}.", weapon.Id);
                throw new Exception("Ocurrió un error al guardar el arma en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear el arma con ID {WeaponId}.", weapon.Id);
                throw;
            }
        }

        public async Task UpdateAsync(Weapon weapon)
        {
            try
            {
                _context.Weapons.Update(weapon);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning(ex, "Conflicto de concurrencia al actualizar el arma {WeaponId}.", weapon.Id);
                throw new Exception("Las estadísticas del arma fueron modificadas por otro proceso. Por favor, recarga e inténtalo de nuevo.", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error de base de datos al intentar actualizar el arma con ID {WeaponId}.", weapon.Id);
                throw new Exception("Ocurrió un error al actualizar el arma.", ex);
            }
        }

        public async Task DeleteAsync(Weapon weapon)
        {
            try
            {
                _context.Weapons.Remove(weapon);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning(ex, "Conflicto de concurrencia al eliminar el arma {WeaponId}.", weapon.Id);
                throw new Exception("El arma ya no existe o fue modificada.", ex);
            }
            catch (DbUpdateException ex)
            {
                // ¡Punto Crítico! Esto se ejecutará si un Fighter tiene esta arma equipada.
                _logger.LogError(ex, "Intento de eliminación de arma referenciada. ID: {WeaponId}.", weapon.Id);
                throw new Exception("No se puede destruir esta arma porque actualmente está siendo utilizada por un peleador. Desequípala primero.", ex);
            }
        }
    }
}
