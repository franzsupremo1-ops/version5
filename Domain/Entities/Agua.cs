using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Agua
    {
        public Guid Id { get; set; }
        public string Zona { get; set; } = string.Empty;
        public int Valor { get; set; }
        public string Unidad { get; set; } = "m3";
        public Guid IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
