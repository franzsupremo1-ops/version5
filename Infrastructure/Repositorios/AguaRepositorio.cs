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
    public class AguaRepositorio:IAgua
    {
        private readonly AppDbContext _appDbContext;

        public AguaRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Actualizar(Agua agua)
        {
            _appDbContext.aguas.Update(agua);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Agua>> All()
        {
            return await _appDbContext.aguas.ToListAsync();
        }

        public async Task Crear(Agua agua)
        {
            _appDbContext.aguas.Add(agua);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Agua> ObtenerId(Guid id)
        {
            return await _appDbContext.aguas.FindAsync(id);
        }

        public async Task Eliminar(Guid id)
        {
            var agua = await ObtenerId(id);
            if (agua != null)
            {
                _appDbContext.aguas.Remove(agua);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
