using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.domain.Domain
{
    public class Weapon
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Damage { get; private set; }
        public int Durability { get; private set; }

        public Weapon(int id, string name, int damage, int durability)
        {
            Id = id;
            Name = name;
            Damage = damage;
            Durability = durability;
        }

        public void UpdateStats(string name, int damage, int durability)
        {
            Name = name;
            Damage = damage;
            Durability = durability;
        }
    }
}
