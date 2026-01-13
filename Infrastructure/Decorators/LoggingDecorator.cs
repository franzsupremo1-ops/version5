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
    public class LoggingDecorator : IAgua
    {
        private readonly IAgua _agua;
        private readonly ILogger<LoggingDecorator> _logger;

        public LoggingDecorator(IAgua agua, ILogger<LoggingDecorator> logger)
        {
            _agua = agua;
            _logger = logger;
        }

        public async Task<IEnumerable<Agua>> All()
        {
            _logger.LogInformation("Listando aguas");
            var result = await _agua.All();

           
            var count = result != null ? result.Count() : 0;
            _logger.LogInformation("Listado {Count} aguas", count);

            return result ?? Enumerable.Empty<Agua>();
        }

        public async Task<Agua> ObtenerId(Guid id)
        {
            _logger.LogInformation("Buscando agua {Id}", id);
            var result = await _agua.ObtenerId(id);
            _logger.LogInformation("Agua encontrada: {Id}", id);
            return result;
        }

        public async Task Crear(Agua agua)
        {
            _logger.LogInformation("Creando agua {Zona}", agua.Zona);
            await _agua.Crear(agua);
            _logger.LogInformation("Creado agua {Zona}", agua.Zona);
        }

        public async Task Actualizar(Agua agua)
        {
            _logger.LogInformation("Actualizando agua {Zona}", agua.Zona);
            await _agua.Actualizar(agua);
            _logger.LogInformation("Actualizado agua {Zona}", agua.Zona);
        }

        public async Task Eliminar(Guid id)
        {
            _logger.LogInformation("Eliminando agua {Id}", id);
            await _agua.Eliminar(id);
            _logger.LogInformation("Eliminado agua {Id}", id);
        }
    }
}
