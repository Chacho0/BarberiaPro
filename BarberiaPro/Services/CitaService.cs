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
            .Where(c => c.Estado == "Pendiente") // Filtra solo las citas pendientes
            .Include(c => c.Usuario) // Incluye la información del usuario
                .ThenInclude(u => u.PerfilCliente) // Incluye el perfil del cliente
            .Include(c => c.Empleado) // Incluye la información del empleado
            .ToListAsync();
    }
    public async Task<List<CitaProcesada>> ObtenerCitasProcesadasPorUsuarioAsync(int idUsuario)
    {
        using var context = _dbContextFactory.CreateDbContext();
        return await context.CitasProcesadas
            .Include(cp => cp.Cita)
                .ThenInclude(c => c.Usuario)
            .Where(cp => cp.Cita.IdUsuario == idUsuario && cp.EstadoPago == "Pendiente")
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

    public async Task CrearCitaProcesadaAsync(CitaProcesada citaProcesada)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.CitasProcesadas.Add(citaProcesada);
        await context.SaveChangesAsync();
    }
    public async Task<List<ClientePagoInfo>> ObtenerClientesConEstadoPagoAsync(int? mes = null, int? año = null)
    {
        using var context = _dbContextFactory.CreateDbContext();
        var query = context.CitasProcesadas
            .Include(cp => cp.Cita)
                .ThenInclude(c => c.Usuario) // Incluir el usuario asociado a la cita
            .AsQueryable();

        // Filtrar por mes y año si se proporcionan
        if (mes.HasValue && año.HasValue)
        {
            query = query.Where(cp => cp.Cita.Fecha.Month == mes && cp.Cita.Fecha.Year == año);
        }

        // Obtener la información de los clientes y su estado de pago
        var clientes = await query
            .Select(cp => new ClientePagoInfo
            {
                NombreCliente = cp.Cita.Usuario.Nombre, // Nombre del cliente
                EstadoPago = cp.EstadoPago, // Estado de pago
                FechaCita = cp.Cita.Fecha // Fecha de la cita
            })
            .ToListAsync();

        return clientes;
    }

    public class ClientePagoInfo
    {
        public string NombreCliente { get; set; } // Nombre del cliente
        public string EstadoPago { get; set; } // Estado de pago (Pagado/Pendiente)
        public DateTime FechaCita { get; set; } // Fecha de la cita
    }
}