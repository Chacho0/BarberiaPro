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
            var empleado = await context.Empleados.FindAsync(idEmpleado);
            if (empleado != null)
            {
                empleado.Estado = estado;
                await context.SaveChangesAsync();
            }
        }
    }
}