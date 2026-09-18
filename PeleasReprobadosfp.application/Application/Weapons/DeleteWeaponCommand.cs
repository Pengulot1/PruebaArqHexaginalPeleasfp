using PeleasReprobadosfp.domain.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeleasReprobadosfp.application.Application.Weapons
{
    public record DeleteWeaponCommand(int Id);

    public class DeleteWeaponCommandHandler
    {
        private readonly IWeaponRepository _repository;

        public DeleteWeaponCommandHandler(IWeaponRepository repository) => _repository = repository;

        public async Task Handle(DeleteWeaponCommand command)
        {
            var weapon = await _repository.GetByIdAsync(command.Id);
            if (weapon == null) throw new Exception("Arma no encontrada xD");

            await _repository.DeleteAsync(weapon); // Recuerda: la infraestructura manejará si está equipada
        }
    }
}
