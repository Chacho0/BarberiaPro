using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BarberiaPro.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdClaim, out var userId))
                {
                    return userId;
                }
                throw new UnauthorizedAccessException("Usuario no autenticado o ID de usuario inválido.");
            }
        }
    }

public interface ICurrentUserService
    {
        int UserId { get; }
    }
}