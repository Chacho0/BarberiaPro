using BarberiaPro.Context;
using BarberiaPro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberiaPro.Services
{
    public class ServicioService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly UserStateService _userStateService;

        public ServicioService(IDbContextFactory<AppDbContext> dbContextFactory, UserStateService userStateService)
        {
            _dbContextFactory = dbContextFactory;
            _userStateService = userStateService;
        }


        // Crear un nuevo servicio (asignar el IdUsuario automáticamente)
        public async Task CrearServicioAsync(Servicio servicio)
        {
            if (_userStateService.UserId == null)
            {
                throw new InvalidOperationException("Usuario no autenticado.");
            }

            servicio.IdUsuario = _userStateService.UserId.Value;

            using var context = _dbContextFactory.CreateDbContext();
            context.Servicios.Add(servicio);
            await context.SaveChangesAsync();
        }

        // Obtener todos los servicios del usuario autenticado
        public async Task<List<Servicio>> ObtenerServiciosAsync()
        {
            if (_userStateService.UserId == null)
            {
                throw new InvalidOperationException("Usuario no autenticado.");
            }

            using var context = _dbContextFactory.CreateDbContext();
            return await context.Servicios
                .Where(s => s.IdUsuario == _userStateService.UserId) // Filtrar por IdUsuario
                .ToListAsync();
        }

        // Buscar servicios por tipo (solo del usuario autenticado)
        public async Task<List<Servicio>> BuscarServiciosAsync(string tipoServicio)
        {
            if (_userStateService.UserId == null)
            {
                throw new InvalidOperationException("Usuario no autenticado.");
            }

            using var context = _dbContextFactory.CreateDbContext();
            return await context.Servicios
                .Where(s => s.TipoServicio.Contains(tipoServicio) && s.IdUsuario == _userStateService.UserId) // Filtrar por IdUsuario
                .ToListAsync();
        }

        // Editar un servicio existente (solo si pertenece al usuario autenticado)
        public async Task EditarServicioAsync(Servicio servicio)
        {
            if (_userStateService.UserId == null)
            {
                throw new InvalidOperationException("Usuario no autenticado.");
            }

            using var context = _dbContextFactory.CreateDbContext();
            var servicioExistente = await context.Servicios
                .FirstOrDefaultAsync(s => s.IdServicio == servicio.IdServicio && s.IdUsuario == _userStateService.UserId);

            if (servicioExistente == null)
            {
                throw new UnauthorizedAccessException("No tienes permiso para editar este servicio.");
            }

            servicioExistente.TipoServicio = servicio.TipoServicio;
            servicioExistente.Precio = servicio.Precio;

            context.Servicios.Update(servicioExistente);
            await context.SaveChangesAsync();
        }

        // Eliminar un servicio (solo si pertenece al usuario autenticado)
        public async Task EliminarServicioAsync(int idServicio)
        {
            if (_userStateService.UserId == null)
            {
                throw new InvalidOperationException("Usuario no autenticado.");
            }

            using var context = _dbContextFactory.CreateDbContext();
            var servicio = await context.Servicios
                .FirstOrDefaultAsync(s => s.IdServicio == idServicio && s.IdUsuario == _userStateService.UserId);

            if (servicio == null)
            {
                throw new UnauthorizedAccessException("No tienes permiso para eliminar este servicio.");
            }

            context.Servicios.Remove(servicio);
            await context.SaveChangesAsync();
        }
    }
}