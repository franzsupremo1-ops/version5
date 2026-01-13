using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Strategies
{
    public class AdminStrategy : IValidacionUsuarioStrategy
    {
        public void Validar(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("Admin: Nombre requerido");

            if (usuario.Nombre.Length < 3)
                throw new ArgumentException("Admin: Nombre mínimo 3 caracteres");

            if (usuario.Rol != "Admin")
                throw new ArgumentException("Debe ser Admin");
        }
    }
}
