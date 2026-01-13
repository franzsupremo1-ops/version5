using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Strategies
{
    public static class AguaStrategyFactory
    {
        public static IValidacionAguaStrategy Crear(string zona)
        {
            return new ZonaDefaultStrategy();
        }
    }
}
