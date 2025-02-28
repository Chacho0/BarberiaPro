using BarberiaPro.Context;
using BarberiaPro.Models;
using Microsoft.EntityFrameworkCore;

public class CitaService
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public CitaService(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
    // Obtener citas pendientes
    public async Task<List<Cita>> ObtenerCitasPendientesAsync()
    {
        using var context = _dbContextFactory.CreateDbContext();
        return await context.Citas
            .Where(c => c.Estado == "Pendiente") 
            .Include(c => c.Usuario) 
            .Include(c => c.Empleado) 
            .ToListAsync();
    }

    // Actualizar una cita
    public async Task ActualizarCitaAsync(Cita cita)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.Citas.Update(cita);
        await context.SaveChangesAsync();
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

    // Crear una nueva cita con múltiples servicios
    public async Task CrearCitaAsync(Cita cita, List<int> idsServicios)
    {
        using var context = _dbContextFactory.CreateDbContext();
        cita.Estado = "Pendiente"; // Estado inicial de la cita

        // Asignar los servicios seleccionados a la cita
        foreach (var idServicio in idsServicios)
        {
            var servicio = await context.Servicios.FindAsync(idServicio);
            if (servicio != null)
            {
                cita.CitaServicios.Add(new CitaServicio { Servicio = servicio });
            }
        }

        context.Citas.Add(cita);
        await context.SaveChangesAsync();
    }
}