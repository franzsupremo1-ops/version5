using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositorios
{
    public class ReporteRepositorio:IReporte
    {
        private readonly AppDbContext _appDbContext;

        public ReporteRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Agua>> All()
        {
            return await _appDbContext.aguas.ToListAsync();
        }

        public async Task<Agua> ObtenerId(Guid id)  // ← AGREGAR ESTE MÉTODO
        {
            return await _appDbContext.aguas.FindAsync(id);
        }
    }
}
