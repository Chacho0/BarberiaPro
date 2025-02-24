using BarberiaPro.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberiaPro.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<CitaProcesada> CitasProcesadas { get; set; }
        public DbSet<HistoriaCliente> HistoriasClientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
      .HasOne(u => u.Empleado) // Un Usuario tiene un Empleado
      .WithOne(e => e.Usuario) // Un Empleado tiene un Usuario
      .HasForeignKey<Empleado>(e => e.IdUsuario) // Clave foránea en Empleado
      .OnDelete(DeleteBehavior.Restrict);  // Comportamiento de eliminación

            // Relación entre Usuario y Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany()
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Empleado y Cargo
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Cargo)
                .WithMany()
                .HasForeignKey(e => e.IdCargo)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Cita y Usuario
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Cita y Empleado
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Empleado)
                .WithMany()
                .HasForeignKey(c => c.IdEmpleado)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Cita y Servicio
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Servicio)
                .WithMany()
                .HasForeignKey(c => c.IdServicio)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Servicio y Usuario
            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.Usuario)
                .WithMany()
                .HasForeignKey(s => s.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre CitaProcesada y Cita
            modelBuilder.Entity<CitaProcesada>()
                .HasOne(cp => cp.Cita)
                .WithMany()
                .HasForeignKey(cp => cp.IdCita)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre HistoriaCliente y Usuario
            modelBuilder.Entity<HistoriaCliente>()
                .HasOne(h => h.Usuario)
                .WithMany()
                .HasForeignKey(h => h.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación entre HistoriaCliente y CitaProcesada
            modelBuilder.Entity<HistoriaCliente>()
                .HasOne(h => h.CitaProcesada)
                .WithMany()
                .HasForeignKey(h => h.IdCitaProcesada)
                .OnDelete(DeleteBehavior.Restrict);

            // **Semillas de datos (Data Seeding)**
            // Sembrar roles
            modelBuilder.Entity<Rol>().HasData(
                new Rol { IdRol = 1, TipoRol = "Admin" },
                new Rol { IdRol = 2, TipoRol = "Cliente" },
                new Rol { IdRol = 3, TipoRol = "Empleado" }
            );

            // Sembrar usuario Admin por defecto
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                IdUsuario = 1,
                Nombre = "Admin",
                Correo = "admin@correo.com",
                PassWord = "Admin123!", // Contraseña encriptada
                RolId = 1
            });
        }
    }
}
