using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.domain.Domain.service
{
    using System;

    namespace PeleasReprobadosfp.domain.Domain.Services
    {
        public class CombatDomainService
        {
            public string SimulateFight(Fighter f1, Weapon? w1, Fighter f2, Weapon? w2)
            {
                var random = new Random();

                // Generar multiplicadores random entre 0.50 y 2.00 (con 2 decimales)
                double multiplier1 = Math.Round(random.NextDouble() * (2.0 - 0.5) + 0.5, 2);
                double multiplier2 = Math.Round(random.NextDouble() * (2.0 - 0.5) + 0.5, 2);

                // Calcular el poder del arma (Daño * Durabilidad). Si no tiene arma, es 0.
                double weaponPower1 = w1 != null ? (w1.Damage * w1.Durability) : 0;
                double weaponPower2 = w2 != null ? (w2.Damage * w2.Durability) : 0;

                // FÓRMULA: ((Fuerza + PoderArma) * MultiplicadorRandom) - VidaEnemigo
                double score1 = ((f1.Strength + weaponPower1) * multiplier1) - f2.Health;
                double score2 = ((f2.Strength + weaponPower2) * multiplier2) - f1.Health;

                // Determinar ganador
                if (score1 > score2)
                {
                    return $"Ganador: {f1.Name} | Puntuaciones -> {f1.Name}: {score1} pts vs {f2.Name}: {score2} pts";
                }
                else if (score2 > score1)
                {
                    return $"Ganador: {f2.Name} | Puntuaciones -> {f2.Name}: {score2} pts vs {f1.Name}: {score1} pts";
                }
                else
                {
                    return $"¡Empate! | Puntuaciones -> {score1} pts vs {score2} pts";
                }
            }
        }
    }
}
