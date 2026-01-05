using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AguaDTOs
    {
        public string Zona { get; set; } = string.Empty;
        public int Valor { get; set; }
        public string Unidad { get; set; } = "m³";
        public Guid IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
