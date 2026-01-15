using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Interfaces
{
    public interface INotificacionService
    {
        // HTTP calls to WebApi
        Task<IEnumerable<NotificacionModel>> ObtenerNotificacionesAsync(int usuarioId);
        Task<int> ObtenerConteoNoLeidasAsync(int usuarioId);
        Task MarcarComoLeidaAsync(int notificacionId);
        Task MarcarVariasComoLeidasAsync(IEnumerable<int> notificacionIds);

        // SignalR connection management
        Task ConectarAsync(int usuarioId);
        Task DesconectarAsync();

        // Event for real-time notifications
        event Action<NotificacionModel> OnNotificacionRecibida;
    }
}
