using Microsoft.EntityFrameworkCore;
using PeleasReprobadosfp.domain.Domain.Interfaces;
using PeleasReprobadosfp.domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PeleasReprobadosfp.infrastructure.Infrastructure
{
    public class FighterRepository : IFighterRepository
    {
        private readonly GameDbContext _context;
        private readonly ILogger<FighterRepository> _logger;

        // Inyectamos el DbContext y un Logger para registrar los errores
        public FighterRepository(GameDbContext context, ILogger<FighterRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Fighter?> GetByIdAsync(int id)
        {
            var rep = await _context.Fighters.FindAsync(id);
            return rep;
        }

        public async Task<IEnumerable<Fighter>> GetAllAsync()
        {
            var rep = await _context.Fighters.ToListAsync();
            return rep;
        }
        public async Task AddAsync(Fighter fighter)
        {
            try
            {
                await _context.Fighters.AddAsync(fighter);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Error típico si hay problemas de restricciones (ej. ID duplicado)
                _logger.LogError(ex, "Error de base de datos al intentar crear el peleador con ID {FighterId}.", fighter.Id);

                // Lanzamos una excepción genérica o personalizada (no la de EF Core)
                throw new Exception("Ocurrió un error al guardar el peleador en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear el peleador con ID {FighterId}.", fighter.Id);
                throw;
            }
        }

        public async Task UpdateAsync(Fighter fighter)
        {
            try
            {
                _context.Fighters.Update(fighter);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // MUY IMPORTANTE en Updates: Ocurre si dos usuarios intentan editar el mismo registro a la vez
                _logger.LogWarning(ex, "Conflicto de concurrencia al actualizar el peleador {FighterId}.", fighter.Id);
                throw new Exception("El registro fue modificado por otro proceso. Por favor, recarga y vuelve a intentarlo.", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error de base de datos al intentar actualizar el peleador con ID {FighterId}.", fighter.Id);
                throw new Exception("Ocurrió un error al actualizar el peleador.", ex);
            }
        }

        public async Task DeleteAsync(Fighter fighter)
        {
            try
            {
                _context.Fighters.Remove(fighter);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Ocurre si intentas eliminar un registro que ya fue eliminado o modificado
                _logger.LogWarning(ex, "Conflicto de concurrencia al eliminar el peleador {FighterId}.", fighter.Id);
                throw new Exception("El peleador ya no existe o fue modificado.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Puede ocurrir si intentas eliminar algo que tiene llaves foráneas estrictas (ej. un arma atada a él)
                _logger.LogError(ex, "Error de base de datos al intentar eliminar el peleador con ID {FighterId}.", fighter.Id);
                throw new Exception("No se puede eliminar el peleador porque está siendo referenciado por otros datos.", ex);
            }
        }
    }
}
