using BarberiaPro.Models;

namespace BarberiaPro.Services
{

    public interface IUsuarioService
    {
        Task<Usuario> AutenticarAsync(string correo, string password);
        Task<Usuario> GetUsuarioAsync(int userId);
        Task<Usuario> GetUsuarioNombre(string nombre);
        Task<List<Usuario>> ObtenerUsuariosAsync();
        Task<TipoRolEnum?> ObtenerRolUsuarioAsync(int userId);
        Task AgregarUsuarioAsync(Usuario usuario);
        Task EditarUsuarioAsync(Usuario usuario);
        Task EliminarUsuarioAsync(int idUsuario);
        Task CambiarEstadoUsuarioAsync(int idUsuario, bool estado);
        Task CambiarRolUsuarioAsync(int idUsuario, int nuevoRolId);
        Task<Usuario?> ObtenerUsuarioActualAsync();
    }

}