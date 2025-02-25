using BarberiaPro.Context;
using BarberiaPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberiaPro.Services
{
    public class CitaService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public CitaService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        // Obtener todos los empleados activos
        public async Task<List<Empleado>> ObtenerEmpleadosActivosAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Empleados
                .Where(e => e.Estado) // Solo empleados activos
                .Include(e => e.Cargo) // Incluir el cargo del empleado
                .ToListAsync();
        }

        // Obtener todos los servicios
        public async Task<List<Servicio>> ObtenerServiciosAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Servicios.ToListAsync();
        }

        // Crear una nueva cita
        public async Task CrearCitaAsync(Cita cita)
        {
            using var context = _dbContextFactory.CreateDbContext();
            cita.Estado = "Pendiente"; // Estado inicial de la cita
            context.Citas.Add(cita);
            await context.SaveChangesAsync();
        }

        // Asignar múltiples servicios a una cita
            //public async Task AsignarServiciosACitaAsync(int idCita, List<int> idsServicios)
            //{
            //    using var context = _dbContextFactory.CreateDbContext();
            //    var cita = await context.Citas
            //        .Include(c => c.Servicio) // Incluir los servicios de la cita
            //        .FirstOrDefaultAsync(c => c.IdCita == idCita);

            //    if (cita != null)
            //    {
            //        foreach (var idServicio in idsServicios)
            //        {
            //            var servicio = await context.Servicios.FindAsync(idServicio);
            //            if (servicio != null)
            //            {
            //                cita.Servicio.Add(servicio); // Asignar el servicio a la cita
            //            }
            //        }
            //        await context.SaveChangesAsync();
            //    }
            //}
    }
}