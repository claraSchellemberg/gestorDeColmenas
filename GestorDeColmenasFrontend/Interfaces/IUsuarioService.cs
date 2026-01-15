using GestorDeColmenasFrontend.Dtos;
using GestorDeColmenasFrontend.Dtos.Usuario;

namespace GestorDeColmenasFrontend.Interfaces
{
    /// <summary>
    /// Servicio para gestionar datos del usuario actual
    /// </summary>
    public interface IUsuarioService
    {
        Task<UsuarioSimpleDto> GetUsuarioActualAsync();
    }
}