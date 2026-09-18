using Google.Cloud.Firestore;
using PeleasReprobadosfp.domain.Domain.Interfaces;
using PeleasReprobadosfp.domain.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PeleasReprobadosfp.infrastructure.Repositories
{
    public class FirestoreWeaponRepository : IWeaponRepository
    {
        private readonly FirestoreDb _db;
        private const string CollectionName = "Weapons";

        public FirestoreWeaponRepository(FirestoreDb db)
        {
            _db = db;
        }

        public async Task AddAsync(Weapon weapon)
        {
            try
            {
                DocumentReference docRef = _db.Collection(CollectionName).Document(weapon.Id.ToString());

                var weaponData = new Dictionary<string, object>
                {
                    { "Id", weapon.Id },
                    { "Name", weapon.Name },
                    { "Damage", weapon.Damage },
                    { "Durability", weapon.Durability }
                };

                await docRef.SetAsync(weaponData);
            }
            catch (Exception ex)
            {
                // Aquí podrías usar un ILogger si lo tienes inyectado
                throw new Exception($"Error al guardar el arma {weapon.Id} en Firestore.", ex);
            }
        }

        public async Task<Weapon?> GetByIdAsync(int id)
        {
            DocumentReference docRef = _db.Collection(CollectionName).Document(id.ToString());
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists) return null;

            Dictionary<string, object> data = snapshot.ToDictionary();

            return new Weapon(
                Convert.ToInt32(data["Id"]),
                data["Name"].ToString(),
                Convert.ToInt32(data["Damage"]),
                Convert.ToInt32(data["Durability"])
            );
        }

        public async Task<IEnumerable<Weapon>> GetAllAsync()
        {
            QuerySnapshot snapshot = await _db.Collection(CollectionName).GetSnapshotAsync();
            var weapons = new List<Weapon>();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Dictionary<string, object> data = document.ToDictionary();
                weapons.Add(new Weapon(
                    Convert.ToInt32(data["Id"]),
                    data["Name"].ToString(),
                    Convert.ToInt32(data["Damage"]),
                    Convert.ToInt32(data["Durability"])
                ));
            }

            return weapons;
        }

        public async Task UpdateAsync(Weapon weapon)
        {
            DocumentReference docRef = _db.Collection(CollectionName).Document(weapon.Id.ToString());

            var updates = new Dictionary<string, object>
            {
                { "Name", weapon.Name },
                { "Damage", weapon.Damage },
                { "Durability", weapon.Durability }
            };

            await docRef.UpdateAsync(updates);
        }

        public async Task DeleteAsync(Weapon weapon)
        {
            DocumentReference docRef = _db.Collection(CollectionName).Document(weapon.Id.ToString());
            await docRef.DeleteAsync();
        }
    }
}
