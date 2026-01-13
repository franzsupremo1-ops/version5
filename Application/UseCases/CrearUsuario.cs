using Application.Strategies;
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
            var strategy = UsuarioStrategyFactory.Crear(usuario.Rol ?? "");
            strategy.Validar(usuario);

            await _usuario.Crear(usuario);
        }
    }
}
