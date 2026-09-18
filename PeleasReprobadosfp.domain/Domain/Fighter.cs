using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.domain.Domain
{
    public class Fighter
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Strength { get; private set; }
        public int? EquippedWeaponId { get; private set; }

        // Constructor: Nace SIN arma (EquippedWeaponId es null por defecto)
        public Fighter(int id, string name, int health, int strength)
        {
            Id = id;
            Name = name;
            Health = health;
            Strength = strength;
        }
        public void UpdateStats(string name, int health, int strength)
        {
            Name = name;
            Health = health;
            Strength = strength;
        }

        public void EquipWeapon(int weaponId) => EquippedWeaponId = weaponId;

        public void ReceiveDamage(int totalDamage)
        {
            Health -= totalDamage;
            if (Health < 0) Health = 0;
        }

    }
}
