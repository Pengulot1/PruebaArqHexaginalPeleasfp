using Microsoft.EntityFrameworkCore;
using PeleasReprobadosfp.domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.infrastructure.Infrastructure
{
    public class GameDbContext : DbContext
    {
        public DbSet<Fighter> Fighters { get; set; }
        public DbSet<Weapon> Weapons { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuraciones de las tablas (Fluent API)
            modelBuilder.Entity<Fighter>(builder => {
                builder.HasKey(f => f.Id);
                builder.Property(f => f.Name).IsRequired().HasMaxLength(100);
                // La relación es opcional, EF Core entiende el Guid?
            });

            modelBuilder.Entity<Weapon>(builder => {
                builder.HasKey(w => w.Id);
                builder.Property(w => w.Name).IsRequired().HasMaxLength(100);
            });
        }
    }
}
