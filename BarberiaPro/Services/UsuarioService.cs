using BarberiaPro.Context;
using BarberiaPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberiaPro.Services
{
    public class UsuarioService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public UsuarioService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Empleado)
                .ToListAsync();
        }

        public async Task AgregarUsuarioAsync(Usuario usuario)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
        }

        public async Task EditarUsuarioAsync(Usuario usuario)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Usuarios.Update(usuario);
            await context.SaveChangesAsync();
        }

        public async Task EliminarUsuarioAsync(int idUsuario)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var usuario = await context.Usuarios.FindAsync(idUsuario);
            if (usuario != null)
            {
                context.Usuarios.Remove(usuario);
                await context.SaveChangesAsync();
            }
        }

        public async Task CambiarEstadoUsuarioAsync(int idUsuario, bool estado)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var usuario = await context.Usuarios
                .Include(u => u.Empleado) // Asegúrate de incluir el empleado
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

            if (usuario != null && usuario.Empleado != null)
            {
                usuario.Empleado.Estado = estado;
                await context.SaveChangesAsync();
            }
        }

        public async Task<Usuario?> ObtenerUsuarioActualAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();
            // Lógica para obtener el usuario actual (puedes usar la autenticación)
            return await context.Usuarios.FirstOrDefaultAsync();
        }
    }
}