using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CrearAgua
    {
        private readonly IAgua _agua;

        public CrearAgua(IAgua agua)
        {
            _agua = agua;
        }

        public async Task EjecutarAsync(Agua agua)
        {
            ValidarAgua(agua);
            await _agua.Crear(agua);
        }

        public void ValidarAgua(Agua agua)
        {
            if (agua is null)
                throw new ArgumentNullException(nameof(agua), "El registro de agua no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(agua.Zona))
                throw new ArgumentException("Existe un error en la zona.");

            if (agua.IdUsuario == Guid.Empty)
                throw new ArgumentException("Existe un error en el IdUsuario: no puede ser vacío.");
        }
    }
}
