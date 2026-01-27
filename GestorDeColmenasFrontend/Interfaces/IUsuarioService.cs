using GestorDeColmenasFrontend.Dtos.Apiario;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Modelos;
using GestorDeColmenasFrontend.Pages;

namespace GestorDeColmenasFrontend.Interfaces
{
    /// Servicio para gestionar autenticación y perfil del usuario
    /// El Id siempre viene de la sesión, NO en los DTOs
    public interface IUsuarioService
    {
        Task<UsuarioSimpleDto?> AutenticarAsync(LoginUsuarioDto credenciales);

        /// <summary>
        /// Obtiene el perfil completo del usuario
        /// El usuarioId viene de la sesión
        /// </summary>
        Task<UsuarioCreateDto?> GetPerfilAsync(int usuarioId);

        /// <summary>
        /// Actualiza el perfil del usuario
        /// El usuarioId viene de la sesión
        /// </summary>
        Task<bool> ActualizarPerfilAsync(int usuarioId, UsuarioCreateDto perfil);

        /// <summary>
        /// Obtiene los datos mínimos del usuario actual para mostrar en header/sidebar
        /// (Id, Nombre, Email, FotoPerfil)
        /// El usuarioId viene de la sesión
        /// </summary>
        Task<UsuarioSimpleDto?> GetUsuarioActualAsync(int usuarioId);

        //para el registro de usuario
        Task<Modelos.RegistroUsuarioModel> RegistrarUsuarioAsync(UsuarioCreateDto dto);
    }
}