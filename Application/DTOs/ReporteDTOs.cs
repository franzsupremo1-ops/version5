using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ReporteDTOs
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Datos { get; set; } = string.Empty;
        public string Formato { get; set; } = "PDF";
        public Guid IdUsuario { get; set; }
    }
}
