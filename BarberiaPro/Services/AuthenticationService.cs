namespace BarberiaPro.Services
{
    using System.Threading.Tasks;
    using BarberiaPro.Context;
    using BarberiaPro.Models;
    // Services/AuthenticationService.cs
    using Microsoft.EntityFrameworkCore;

    public class AuthenticationService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public AuthenticationService(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<LoginResult> LoginAsync(string correo, string password)
        {
            await _semaphore.WaitAsync(); // Bloquear el acceso

            try
            {
                using var context = _dbContextFactory.CreateDbContext();

                var usuario = await context.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo == correo && u.PassWord == password);

                if (usuario == null)
                {
                    return new LoginResult { Success = false, ErrorMessage = "Credenciales inválidas" };
                }

                return new LoginResult { Success = true };
            }
            finally
            {
                _semaphore.Release(); // Liberar el acceso
            }
        }
    }
    public class LoginResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}