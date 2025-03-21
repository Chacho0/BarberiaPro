﻿using BarberiaPro.Context;
using BarberiaPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BarberiaPro.Services
{
    public class PerfilClienteService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public PerfilClienteService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<Perfil> ObtenerPerfilClienteAsync(int idUsuario)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.perfilCliente
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(p => p.IdUsuario == idUsuario) ?? new Perfil();
        }


        public async Task GuardarPerfilClienteAsync(Perfil perfil)
        {
            using var context = _dbContextFactory.CreateDbContext();
            if (perfil.IdPerfil == 0)
            {
                context.perfilCliente.Add(perfil);
            }
            else
            {
                context.perfilCliente.Update(perfil);
            }
            await context.SaveChangesAsync();
        }
    }
}