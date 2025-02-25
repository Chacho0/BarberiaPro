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

                return new LoginResult { Success = true, UserId = usuario.IdUsuario }; // Devolver el ID del usuario
            }
            finally
            {
                _semaphore.Release(); // Liberar el acceso
            }
        }

        public async Task<LoginResult> RegistrarUsuarioAsync(Usuario usuario)
        {
            using var context = _dbContextFactory.CreateDbContext();

            // Verificar si el correo ya está registrado
            var usuarioExistente = await context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == usuario.Correo);

            if (usuarioExistente != null)
            {
                return new LoginResult { Success = false, ErrorMessage = "El correo ya está registrado." };
            }

            // Guardar el nuevo usuario
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            return new LoginResult { Success = true };
        }
    }
    public class LoginResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public int UserId { get; set; } // Añadir el ID del usuario
    }
}