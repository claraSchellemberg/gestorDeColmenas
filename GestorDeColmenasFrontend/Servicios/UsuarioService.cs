using GestorDeColmenasFrontend.Dtos;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Interfaces;

namespace GestorDeColmenasFrontend.Servicios
{
    /// <summary>
    /// Implementación del servicio de usuario
    /// Por ahora devuelve datos ficticios, luego se conectará al backend
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http)
        {
            _http = http;
        }

        public Task<UsuarioSimpleDto> GetUsuarioActualAsync()
        {
            throw new NotImplementedException("Backend no conectado. Usar DatosFicticios en el PageModel.");
        }
    }
}