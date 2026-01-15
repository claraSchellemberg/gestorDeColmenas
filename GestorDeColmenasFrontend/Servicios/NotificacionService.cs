using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Modelos;
using Microsoft.AspNetCore.SignalR.Client;

namespace GestorDeColmenasFrontend.Servicios
{
    public class NotificacionService : INotificacionService
    {
        private readonly HttpClient _http;
        private readonly ILogger<NotificacionService> _logger;
        private HubConnection? _hubConnection;
        private readonly string _hubUrl;
        public NotificacionService(HttpClient http, ILogger<NotificacionService> logger)
        {
            _http = http;
            _logger = logger;
            var baseUrl = _http.BaseAddress?.ToString()?.TrimEnd('/') ?? "http://localhost:5083";
            _hubUrl = $"{baseUrl}/notificacionesHub";
        }

        public event Action<NotificacionModel> OnNotificacionRecibida;

        public async Task ConectarAsync(int usuarioId)
        {
            if(_hubConnection is not null)
            {
                return;
            }
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{_hubUrl}?usuarioId={usuarioId}")
                .WithAutomaticReconnect()
                .Build();
            _hubConnection.On<NotificacionModel>("RecibirNotificacion", (notificacion) =>
            {
               OnNotificacionRecibida?.Invoke(notificacion);
            });
            try
            {
                await _hubConnection.StartAsync();
                _logger.LogInformation("Conectado al hub de notificaciones.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al conectar al hub de notificaciones.");
            }
        }

        public async Task DesconectarAsync()
        {
            if (_hubConnection is not null)
            {
                await _hubConnection.DisposeAsync();
                _hubConnection = null;
            }
        }

        public async Task MarcarComoLeidaAsync(int notificacionId)
        {
            var resp = await _http.PutAsync($"Notificaciones/{notificacionId}/marcarLeida", null);
            if(!resp.IsSuccessStatusCode)
            {
                var error = await resp.Content.ReadAsStringAsync();
                _logger.LogError($"Error al marcar la notificación {notificacionId} como leída. Código de estado: {resp.StatusCode}", notificacionId, error);
            }
        }

        public async Task MarcarVariasComoLeidasAsync(IEnumerable<int> notificacionIds)
        {
            var resp = await _http.PutAsJsonAsync("Notificaciones/marcarVariasLeidas", notificacionIds);
            if (!resp.IsSuccessStatusCode)
            {
                var error = await resp.Content.ReadAsStringAsync();
                _logger.LogError($"Error al marcar las notificaciones como leída. Código de estado: {resp.StatusCode}", notificacionIds, error);
            }
        }

        public async Task<int> ObtenerConteoNoLeidasAsync(int usuarioId)
        {
            var resp = await _http.GetAsync($"Notificaciones/{usuarioId}/conteoNoLeidas");
            if (resp.IsSuccessStatusCode)
            {
                return await resp.Content.ReadFromJsonAsync<int>();
            }
            else
            {
                var error = await resp.Content.ReadAsStringAsync();
                _logger.LogError($"Error al obtener el conteo de notificaciones no leídas para el usuario {usuarioId}. Código de estado: {resp.StatusCode}", usuarioId, error);
                return 0;
            }
        }

        public async Task<IEnumerable<NotificacionModel>> ObtenerNotificacionesAsync(int usuarioId)
        {
            var resp = await _http.GetAsync($"Notificaciones/usuario/{usuarioId}");
            if (resp.IsSuccessStatusCode)
            {
                var notificaciones = await resp.Content.ReadFromJsonAsync<IEnumerable<NotificacionModel>>();
                return notificaciones ?? Enumerable.Empty<NotificacionModel>();
            }
            else
            {
                var error = await resp.Content.ReadAsStringAsync();
                _logger.LogError($"Error obteniendo notificaciones: {(int)resp.StatusCode}");
                return Enumerable.Empty<NotificacionModel>();
            }
        }
    }
}
