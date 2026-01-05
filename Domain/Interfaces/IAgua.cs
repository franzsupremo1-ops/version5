using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAgua
    {
        Task<IEnumerable<Agua>> All();
        Task<Agua> ObtenerId(Guid id);
        Task Crear(Agua agua);
        Task Actualizar(Agua agua);
        Task Eliminar(Guid id);
    }
}
