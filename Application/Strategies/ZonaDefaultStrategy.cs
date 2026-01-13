using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Strategies
{
    public class ZonaDefaultStrategy : IValidacionAguaStrategy
    {
        public void Validar(Agua agua)
        {
          
            if (agua == null)
                throw new ArgumentNullException(nameof(agua), "Agua no puede ser nula");

            if (string.IsNullOrWhiteSpace(agua.Zona))
                throw new ArgumentException("Zona es requerida (cualquier nombre)");

           
            if (agua.Valor < 0)
                throw new ArgumentException("Valor debe ser número positivo (≥ 0)");

            if (agua.Valor == 0)
                throw new ArgumentException("Valor no puede ser 0");

           
            if (agua.IdUsuario == Guid.Empty)
                throw new ArgumentException("IdUsuario es requerido");

            
            if (agua.Unidad != "m3")
                throw new ArgumentException("Unidad debe ser m3");
        }
    }
}
