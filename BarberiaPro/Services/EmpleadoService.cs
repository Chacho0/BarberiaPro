using BarberiaPro.Context;
using BarberiaPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BarberiaPro.Services
{
    public class EmpleadoService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public EmpleadoService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CambiarEstadoEmpleadoAsync(int idEmpleado, bool estado)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var empleado = await context.Empleados
                .Include(e => e.Usuario) // Incluir usuario relacionado
                .FirstOrDefaultAsync(e => e.IdEmpleado == idEmpleado);

            if (empleado != null && empleado.Usuario != null)
            {
                empleado.Estado = estado;
                await context.SaveChangesAsync();
            }
        }
        public async Task<Empleado> ObtenerEmpleadoAsync(int idUsuario)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Empleados
                .Include(e => e.Usuario)
                .Include(e => e.Cargo)
                .FirstOrDefaultAsync(e => e.IdUsuario == idUsuario) ?? new Empleado();
        }

        public async Task GuardarEmpleadoAsync(Empleado empleado)
        {
            using var context = _dbContextFactory.CreateDbContext();
            if (empleado.IdEmpleado == 0)
            {
                context.Empleados.Add(empleado);
            }
            else
            {
                context.Empleados.Update(empleado);
            }
            await context.SaveChangesAsync();
        }
    }
}