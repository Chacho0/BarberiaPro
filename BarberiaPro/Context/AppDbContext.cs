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
        public DbSet<Perfil> perfilCliente { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<CitaProcesada> CitasProcesadas { get; set; }
        public DbSet<HistoriaCliente> HistoriasClientes { get; set; }
        public DbSet<CitaServicio> CitaServicios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Empleado)
                .WithOne(e => e.Usuario)
                .HasForeignKey<Empleado>(e => e.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany()
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.PerfilCliente)
                .WithOne(p => p.Usuario)
                .HasForeignKey<Perfil>(p => p.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Cargo)
                .WithMany()
                .HasForeignKey(e => e.IdCargo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Empleado)
                .WithMany()
                .HasForeignKey(c => c.IdEmpleado)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.Usuario)
                .WithMany()
                .HasForeignKey(s => s.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CitaProcesada>()
                .HasOne(cp => cp.Cita)
                .WithMany()
                .HasForeignKey(cp => cp.IdCita)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistoriaCliente>()
                .HasOne(h => h.Usuario)
                .WithMany()
                .HasForeignKey(h => h.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistoriaCliente>()
                .HasOne(h => h.CitaProcesada)
                .WithMany()
                .HasForeignKey(h => h.IdCitaProcesada)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CitaServicio>()
                .HasKey(cs => new { cs.IdCita, cs.IdServicio });

            modelBuilder.Entity<CitaServicio>()
                .HasOne(cs => cs.Cita)
                .WithMany(c => c.CitaServicios)
                .HasForeignKey(cs => cs.IdCita);

            modelBuilder.Entity<CitaServicio>()
                .HasOne(cs => cs.Servicio)
                .WithMany(s => s.CitaServicios)
                .HasForeignKey(cs => cs.IdServicio);

            modelBuilder.Entity<Rol>().HasData(
                new Rol { IdRol = 1, TipoRol = "Admin" },
                new Rol { IdRol = 2, TipoRol = "Cliente" },
                new Rol { IdRol = 3, TipoRol = "Empleado" }
            );

            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                IdUsuario = 1,
                Nombre = "Admin",
                Correo = "admin@correo.com",
                PassWord = "Admin123!",
                RolId = 1
            });
        }
    }
}