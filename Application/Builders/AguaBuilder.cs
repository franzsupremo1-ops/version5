using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Builders
{
    public class AguaBuilder
    {
        private readonly Agua _agua = new();

        public AguaBuilder ConZona(string zona)
        {
            _agua.Zona = zona;
            return this;
        }

        public AguaBuilder ConValor(int valor)
        {
            _agua.Valor = valor;
            return this;
        }

        public AguaBuilder ConUsuario(Guid idUsuario)
        {
            _agua.IdUsuario = idUsuario;
            return this;
        }

        public Agua Build()
        {
            return _agua;
        }
    }
}
