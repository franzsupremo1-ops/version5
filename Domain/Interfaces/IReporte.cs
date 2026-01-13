using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IReporte  // ← Renombra a IUsuarioRepository si quieres
    {
        Task<IEnumerable<Agua>> All();  // ← AGUA completa
        Task<Agua> ObtenerId(Guid id);  // ← AGREGAR ESTE MÉTODO
        
    }
}
