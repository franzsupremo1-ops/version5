using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Strategies
{
    public static class UsuarioStrategyFactory
    {
        public static IValidacionUsuarioStrategy Crear(string rol)
        {
            return rol.ToLower() switch
            {
                "admin" => new AdminStrategy(),
                "cliente" => new ClienteStrategy(),
                "gerente" => new GerenteStrategy(),
                _ => throw new ArgumentException($"Rol {rol} no válido")
            };
        }
    }
}
