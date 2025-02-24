using BarberiaPro.Context;
using BarberiaPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberiaPro.Services
{
    public class CargoService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public CargoService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Cargo>> ObtenerCargosAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Cargos.ToListAsync();
        }

        public async Task AgregarCargoAsync(Cargo cargo)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.Cargos.Add(cargo);
            await context.SaveChangesAsync();
        }
    }
}