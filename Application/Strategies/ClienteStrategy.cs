using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Strategies
{
    public class ClienteStrategy : IValidacionUsuarioStrategy
    {
        public void Validar(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("Cliente: Nombre requerido");

            if (string.IsNullOrWhiteSpace(usuario.Apellido))
                throw new ArgumentException("Cliente: Apellido requerido");
        }
    }
}
