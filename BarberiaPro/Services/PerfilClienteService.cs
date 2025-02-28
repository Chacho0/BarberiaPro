using BarberiaPro.Context;
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
      

        public async Task GuardarPerfilClienteAsync(Perfil perfilCliente)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var perfilExistente = await context.perfilCliente
                .FirstOrDefaultAsync(p => p.IdPerfil == perfilCliente.IdPerfil);

            if (perfilExistente != null)
            {
                perfilExistente.Direccion = perfilCliente.Direccion;
                perfilExistente.Telefono = perfilCliente.Telefono;
                perfilExistente.Foto = perfilCliente.Foto;
                context.perfilCliente.Update(perfilExistente);
            }
            else
            {
                context.perfilCliente.Add(perfilCliente);
            }

            await context.SaveChangesAsync();
        }
    }
}