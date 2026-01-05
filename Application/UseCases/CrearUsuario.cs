using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class CrearUsuario
    {
        private readonly IUsuario _usuario;

        public CrearUsuario(IUsuario usuario)
        {
            _usuario = usuario;
        }

        public async Task EjecutarAsync(Usuario usuario)
        {
            ValidarUsuario(usuario);
            await _usuario.Crear(usuario);
        }

        public void ValidarUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("Existe un error en el nombre");

            if (string.IsNullOrWhiteSpace(usuario.Rol))
                throw new ArgumentException("Existe un error en el rol");
        }
    }
}
