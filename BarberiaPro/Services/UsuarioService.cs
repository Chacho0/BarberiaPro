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

        public async Task<Usuario> GetUsuarioAsync(int userId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Usuarios.FindAsync(userId);
        }

        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Empleado)
                .ToListAsync();
        }
        public async Task<TipoRolEnum?> ObtenerRolUsuarioAsync(int userId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var usuario = await context.Usuarios
                .Include(u => u.Rol) // Incluir el rol del usuario
                .FirstOrDefaultAsync(u => u.IdUsuario == userId);

            if (usuario?.Rol == null)
            {
                return null; // Si no se encuentra el usuario o el rol, devolver null
            }

            // Convertir el nombre del rol al enum TipoRolEnum
            if (Enum.TryParse(usuario.Rol.TipoRol, out TipoRolEnum rol))
            {
                return rol;
            }

            return null; // Si no se puede convertir, devolver null
        }

        public async Task AgregarUsuarioAsync(Usuario usuario)
        {
            using var context = _dbContextFactory.CreateDbContext();

            // Verificar si el rol existe
            var rolExistente = await context.Roles.AnyAsync(r => r.IdRol == usuario.RolId);
            if (!rolExistente)
            {
                throw new InvalidOperationException("El rol especificado no existe.");
            }

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
            var usuario = await context.Usuarios
                .Include(u => u.Empleado) // Incluir empleado si existe
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

            if (usuario != null)
            {
                // Eliminar empleado relacionado si existe
                if (usuario.Empleado != null)
                {
                    context.Empleados.Remove(usuario.Empleado);
                }

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
        public async Task CambiarRolUsuarioAsync(int idUsuario, int nuevoRolId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            // Verificar si el nuevo rol existe
            var rolExistente = await context.Roles.AnyAsync(r => r.IdRol == nuevoRolId);
            if (!rolExistente)
            {
                throw new InvalidOperationException("El rol especificado no existe.");
            }

            var usuario = await context.Usuarios.FindAsync(idUsuario);
            if (usuario != null)
            {
                usuario.RolId = nuevoRolId;
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