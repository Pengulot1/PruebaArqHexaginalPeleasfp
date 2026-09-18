using Google.Cloud.Firestore;
using PeleasReprobadosfp.domain.Domain.Interfaces;
using PeleasReprobadosfp.domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.infrastructure.Infrastructure.Repositories
{
    public class FirestoreFighterRepository : IFighterRepository
    {
        private readonly FirestoreDb _db;
        private const string CollectionName = "Fighters";

        // Inyectamos la conexión a Firestore
        public FirestoreFighterRepository(FirestoreDb db)
        {
            _db = db;
        }

        public async Task AddAsync(Fighter fighter)
        {
            // Firestore usa strings para los IDs de los documentos.
            DocumentReference docRef = _db.Collection(CollectionName).Document(fighter.Id.ToString());

            // Mapeamos manualmente para no contaminar el Dominio con atributos de Firebase
            var fighterData = new Dictionary<string, object>
            {
                { "Id", fighter.Id },
                { "Name", fighter.Name },
                { "Health", fighter.Health },
                { "Strength", fighter.Strength },
                { "EquippedWeaponId", fighter.EquippedWeaponId } // Puede ser null
            };

            await docRef.SetAsync(fighterData);
        }

        public async Task<Fighter?> GetByIdAsync(int id)
        {
            DocumentReference docRef = _db.Collection(CollectionName).Document(id.ToString());
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists) return null;

            // Reconstruimos la entidad del Dominio a partir de los datos de Firestore
            Dictionary<string, object> data = snapshot.ToDictionary();

            var fighter = new Fighter(
                Convert.ToInt32(data["Id"]),
                data["Name"].ToString(),
                Convert.ToInt32(data["Health"]),
                Convert.ToInt32(data["Strength"])
            );

            // Si tiene un arma equipada, se la asignamos
            if (data.ContainsKey("EquippedWeaponId") && data["EquippedWeaponId"] != null)
            {
                // Dependiendo de cómo lo guarde, puede requerir un cast a int o Guid
                fighter.EquipWeapon(Convert.ToInt32(data["EquippedWeaponId"]));
            }

            return fighter;
        }

        public async Task UpdateAsync(Fighter fighter)
        {
            DocumentReference docRef = _db.Collection(CollectionName).Document(fighter.Id.ToString());

            var updates = new Dictionary<string, object>
            {
                { "Name", fighter.Name },
                { "Health", fighter.Health },
                { "Strength", fighter.Strength },
                { "EquippedWeaponId", fighter.EquippedWeaponId }
            };

            await docRef.UpdateAsync(updates);
        }

        public async Task DeleteAsync(Fighter fighter)
        {
            DocumentReference docRef = _db.Collection(CollectionName).Document(fighter.Id.ToString());
            await docRef.DeleteAsync();
        }

        public async Task<IEnumerable<Fighter>> GetAllAsync()
        {
            CollectionReference collectionRef = _db.Collection(CollectionName);
            QuerySnapshot snapshot = await collectionRef.GetSnapshotAsync();

            var fighters = new List<Fighter>();

            foreach (DocumentSnapshot document in snapshot.Documents)
            {
                Dictionary<string, object> data = document.ToDictionary();
                var fighter = new Fighter(
                    Convert.ToInt32(data["Id"]),
                    data["Name"].ToString(),
                    Convert.ToInt32(data["Health"]),
                    Convert.ToInt32(data["Strength"])
                );

                if (data.ContainsKey("EquippedWeaponId") && data["EquippedWeaponId"] != null)
                {
                    fighter.EquipWeapon(Convert.ToInt32(data["EquippedWeaponId"]));
                }

                fighters.Add(fighter);
            }

            return fighters;
        }
    }
}

