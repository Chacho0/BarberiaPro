﻿// <auto-generated />
using System;
using BarberiaPro.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BarberiaPro.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BarberiaPro.Models.Cargo", b =>
                {
                    b.Property<int>("IdCargo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCargo"));

                    b.Property<string>("DescripcionCargo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCargo");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("BarberiaPro.Models.Cita", b =>
                {
                    b.Property<int>("IdCita")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCita"));

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Hora")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdEmpleado")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.HasKey("IdCita");

                    b.HasIndex("IdEmpleado");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("BarberiaPro.Models.CitaProcesada", b =>
                {
                    b.Property<int>("IdCitaProcesada")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCitaProcesada"));

                    b.Property<string>("EstadoPago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCita")
                        .HasColumnType("int");

                    b.Property<string>("NumeroTransferencia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Point")
                        .HasColumnType("int");

                    b.Property<bool>("Seleccionada")
                        .HasColumnType("bit");

                    b.Property<decimal>("TotalPago")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("VoucherPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCitaProcesada");

                    b.HasIndex("IdCita");

                    b.ToTable("CitasProcesadas");
                });

            modelBuilder.Entity("BarberiaPro.Models.CitaServicio", b =>
                {
                    b.Property<int>("IdCita")
                        .HasColumnType("int");

                    b.Property<int>("IdServicio")
                        .HasColumnType("int");

                    b.HasKey("IdCita", "IdServicio");

                    b.HasIndex("IdServicio");

                    b.ToTable("CitaServicios");
                });

            modelBuilder.Entity("BarberiaPro.Models.Empleado", b =>
                {
                    b.Property<int>("IdEmpleado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEmpleado"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<int>("IdCargo")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdEmpleado");

                    b.HasIndex("IdCargo");

                    b.HasIndex("IdUsuario")
                        .IsUnique();

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("BarberiaPro.Models.HistoriaCliente", b =>
                {
                    b.Property<int>("IdHistory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHistory"));

                    b.Property<int>("IdCitaProcesada")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.HasKey("IdHistory");

                    b.HasIndex("IdCitaProcesada");

                    b.HasIndex("IdUsuario");

                    b.ToTable("HistoriasClientes");
                });

            modelBuilder.Entity("BarberiaPro.Models.Perfil", b =>
                {
                    b.Property<int>("IdPerfil")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPerfil"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPerfil");

                    b.HasIndex("IdUsuario")
                        .IsUnique();

                    b.ToTable("perfilCliente");
                });

            modelBuilder.Entity("BarberiaPro.Models.Rol", b =>
                {
                    b.Property<int>("IdRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRol"));

                    b.Property<string>("TipoRol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRol");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            IdRol = 1,
                            TipoRol = "Admin"
                        },
                        new
                        {
                            IdRol = 2,
                            TipoRol = "Cliente"
                        },
                        new
                        {
                            IdRol = 3,
                            TipoRol = "Empleado"
                        });
                });

            modelBuilder.Entity("BarberiaPro.Models.Servicio", b =>
                {
                    b.Property<int>("IdServicio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdServicio"));

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TipoServicio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdServicio");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Servicios");
                });

            modelBuilder.Entity("BarberiaPro.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<int?>("RolIdRol")
                        .HasColumnType("int");

                    b.HasKey("IdUsuario");

                    b.HasIndex("RolId");

                    b.HasIndex("RolIdRol");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            IdUsuario = 1,
                            Correo = "admin@correo.com",
                            Nombre = "Admin",
                            PassWord = "Admin123!",
                            RolId = 1
                        });
                });

            modelBuilder.Entity("BarberiaPro.Models.Cita", b =>
                {
                    b.HasOne("BarberiaPro.Models.Empleado", "Empleado")
                        .WithMany()
                        .HasForeignKey("IdEmpleado")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BarberiaPro.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Empleado");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BarberiaPro.Models.CitaProcesada", b =>
                {
                    b.HasOne("BarberiaPro.Models.Cita", "Cita")
                        .WithMany()
                        .HasForeignKey("IdCita")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cita");
                });

            modelBuilder.Entity("BarberiaPro.Models.CitaServicio", b =>
                {
                    b.HasOne("BarberiaPro.Models.Cita", "Cita")
                        .WithMany("CitaServicios")
                        .HasForeignKey("IdCita")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BarberiaPro.Models.Servicio", "Servicio")
                        .WithMany("CitaServicios")
                        .HasForeignKey("IdServicio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cita");

                    b.Navigation("Servicio");
                });

            modelBuilder.Entity("BarberiaPro.Models.Empleado", b =>
                {
                    b.HasOne("BarberiaPro.Models.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("IdCargo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BarberiaPro.Models.Usuario", "Usuario")
                        .WithOne("Empleado")
                        .HasForeignKey("BarberiaPro.Models.Empleado", "IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BarberiaPro.Models.HistoriaCliente", b =>
                {
                    b.HasOne("BarberiaPro.Models.CitaProcesada", "CitaProcesada")
                        .WithMany()
                        .HasForeignKey("IdCitaProcesada")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BarberiaPro.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CitaProcesada");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BarberiaPro.Models.Perfil", b =>
                {
                    b.HasOne("BarberiaPro.Models.Usuario", "Usuario")
                        .WithOne("PerfilCliente")
                        .HasForeignKey("BarberiaPro.Models.Perfil", "IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BarberiaPro.Models.Servicio", b =>
                {
                    b.HasOne("BarberiaPro.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BarberiaPro.Models.Usuario", b =>
                {
                    b.HasOne("BarberiaPro.Models.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BarberiaPro.Models.Rol", null)
                        .WithMany("Usuarios")
                        .HasForeignKey("RolIdRol");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("BarberiaPro.Models.Cita", b =>
                {
                    b.Navigation("CitaServicios");
                });

            modelBuilder.Entity("BarberiaPro.Models.Rol", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("BarberiaPro.Models.Servicio", b =>
                {
                    b.Navigation("CitaServicios");
                });

            modelBuilder.Entity("BarberiaPro.Models.Usuario", b =>
                {
                    b.Navigation("Empleado");

                    b.Navigation("PerfilCliente");
                });
#pragma warning restore 612, 618
        }
    }
}
