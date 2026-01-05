using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reporte
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Datos { get; set; } = string.Empty;
        public string Formato { get; set; } = "PDF";
        public Guid UsuarioId { get; set; }
        public ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();
    }
}
