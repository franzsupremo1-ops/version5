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
    public class UsuarioRepositorio:IUsuario
    {
        private readonly AppDbContext _appDbContext;

        public UsuarioRepositorio(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Actualizar(Usuario usuario)
        {
            _appDbContext.usuario.Update(usuario);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> All()
        {
            return await _appDbContext.usuario.ToListAsync();
        }

        public async Task Crear(Usuario usuario)
        {
            _appDbContext.usuario.Add(usuario);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Usuario> ObtenerId(Guid id)
        {
            return await _appDbContext.usuario.FindAsync(id); 
        }


        public async Task Eliminar(Guid id)
        {
            var usuario = await ObtenerId(id);
            if (usuario != null)
            {
                _appDbContext.usuario.Remove(usuario);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
