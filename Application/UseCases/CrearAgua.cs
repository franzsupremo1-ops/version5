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
    public class CrearAgua
    {
        private readonly IAgua _agua;

        public CrearAgua(IAgua agua)
        {
            _agua = agua;
        }

        public async Task EjecutarAsync(Agua agua)
        {
            var strategy = AguaStrategyFactory.Crear(agua.Zona);
            strategy.Validar(agua);

            await _agua.Crear(agua);
        }
    }
}
