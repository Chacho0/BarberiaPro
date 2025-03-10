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
    public async Task<List<Cita>> ObtenerCitasAsync()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        return await context.Citas.ToListAsync();
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
    // Actualizar una cita
    public async Task ActualizarCitaAsync(Cita cita)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.Citas.Update(cita);
        await context.SaveChangesAsync();
    }
    public async Task<List<Cita>> ObtenerCitasPorEmpleadoAsync(int idEmpleado)
    {
        using var context = _dbContextFactory.CreateDbContext();
        return await context.Citas
            .Where(c => c.IdEmpleado == idEmpleado) // Filtrar por empleado
            .Include(c => c.Usuario) // Incluir información del cliente
            .Include(c => c.CitaServicios) // Incluir servicios asociados
                .ThenInclude(cs => cs.Servicio)
            .ToListAsync();
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
    public async Task<List<CitaInfo>> ObtenerCitasConDetallesAsync()
    {
        using var context = _dbContextFactory.CreateDbContext(); // Crear una instancia del contexto

        var citas = await context.Citas // Usar el contexto creado
            .Include(c => c.Usuario) // Incluir el cliente
            .Include(c => c.Empleado) // Incluir el empleado
            .Include(c => c.CitaServicios) // Incluir los servicios
            .ThenInclude(cs => cs.Servicio) // Incluir el detalle de los servicios
            .Select(c => new CitaInfo
            {
                NombreCliente = c.Usuario.Nombre,
                NombreEmpleado = c.Empleado.Nombre,
                FechaCita = c.Fecha,
                HoraCita = c.Hora,
                Estado = c.Estado,
                Servicios = c.CitaServicios.Select(cs => cs.Servicio.TipoServicio).ToList()
            })
            .ToListAsync();

        return citas;
    }
    public async Task CrearCitaProcesadaAsync(CitaProcesada citaProcesada)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.CitasProcesadas.Add(citaProcesada);
        await context.SaveChangesAsync();
    }
    public async Task<List<CitaProcesada>> ObtenerCitasProcesadasPorUsuarioAsync(int idUsuario)
    {
        using var context = _dbContextFactory.CreateDbContext();
        var citasProcesadas = await context.CitasProcesadas
            .Include(cp => cp.Cita)
                .ThenInclude(c => c.Usuario)
            .Include(cp => cp.Cita)
                .ThenInclude(c => c.CitaServicios)
                    .ThenInclude(cs => cs.Servicio) // Incluir los servicios asociados a la cita
            .Where(cp => cp.Cita.IdUsuario == idUsuario) // Obtener todas las citas del usuario
            .ToListAsync();

        // Calcular el total a pagar para cada cita procesada
        foreach (var citaProcesada in citasProcesadas)
        {
            citaProcesada.TotalPago = citaProcesada.Cita.CitaServicios.Sum(cs => cs.Servicio.Precio);
        }

        return citasProcesadas;
    }
    public async Task ActualizarCitaProcesadaAsync(CitaProcesada citaProcesada)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.CitasProcesadas.Update(citaProcesada);
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
                EstadoPago = cp.EstadoPago.ToString(), // Convertir el enum a string
                FechaCita = cp.Cita.Fecha, // Fecha de la cita
                NumeroTransferencia = cp.NumeroTransferencia,
                VoucherPath = cp.VoucherPath,
                Observacion = cp.Observacion
            })
            .ToListAsync();

        return clientes;
    }

    public async Task ActualizarCitaProcesada(CitaProcesada citaProcesada)
    {
        using var context = _dbContextFactory.CreateDbContext();
        context.CitasProcesadas.Update(citaProcesada);
        await context.SaveChangesAsync();
    }

    public async Task<CitaProcesada> ObtenerCitaProcesadaPorClienteAsync(string nombreCliente)
    {
        using var context = _dbContextFactory.CreateDbContext();
        return await context.CitasProcesadas
            .Include(cp => cp.Cita)
                .ThenInclude(c => c.Usuario)
            .FirstOrDefaultAsync(cp => cp.Cita.Usuario.Nombre == nombreCliente);
    }
    public class ClientePagoInfo
    {
       
        public string NombreCliente { get; set; } // Nombre del cliente
        public string EstadoPago { get; set; } // Estado de pago (Pagado/Pendiente)
        public DateTime FechaCita { get; set; } // Fecha de la cita
        public string NumeroTransferencia { get; set; }
        public string? VoucherPath { get; set; } // Ruta del voucher (imagen)
        public string? Observacion { get; set; }

       
    }
    public class CitaInfo
    {
        public string NombreCliente { get; set; }
        public string NombreEmpleado { get; set; }
        public DateTime FechaCita { get; set; }
        public string HoraCita { get; set; }
        public string Estado { get; set; }
        public List<string> Servicios { get; set; } = new List<string>();
    }
}