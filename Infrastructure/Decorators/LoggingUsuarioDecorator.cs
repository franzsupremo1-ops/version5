using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Decorators
{
    public class LoggingUsuarioDecorator : IUsuario
    {
        private readonly IUsuario _usuario;
        private readonly ILogger<LoggingUsuarioDecorator> _logger;

        public LoggingUsuarioDecorator(IUsuario usuario, ILogger<LoggingUsuarioDecorator> logger)
        {
            _usuario = usuario;
            _logger = logger;
        }

        public async Task<IEnumerable<Usuario>> All()
        {
            _logger.LogInformation("Listando todos los usuarios");
            var result = await _usuario.All();
            _logger.LogInformation("Listado {Count} usuarios", result.Count());
            return result;
        }

        public async Task<Usuario> ObtenerId(Guid id)
        {
            _logger.LogInformation("Buscando usuario ID: {Id}", id);
            var result = await _usuario.ObtenerId(id);
            return result;
        }

        public async Task Crear(Usuario usuario)
        {
            _logger.LogInformation("Creando usuario: {Nombre}", usuario.Nombre);
            await _usuario.Crear(usuario);
            _logger.LogInformation("Creado usuario: {Nombre}", usuario.Nombre);
        }

        public async Task Actualizar(Usuario usuario)
        {
            _logger.LogInformation("Actualizando usuario: {Nombre}", usuario.Nombre);
            await _usuario.Actualizar(usuario);
            _logger.LogInformation("Actualizado usuario: {Nombre}", usuario.Nombre);
        }

        public async Task Eliminar(Guid id)
        {
            _logger.LogInformation("Eliminando usuario ID: {Id}", id);
            await _usuario.Eliminar(id);
            _logger.LogInformation("Eliminado usuario ID: {Id}", id);
        }
    }
}
