using GestorDeColmenasFrontend.Dtos.Apiario;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Servicios
{
    public class ApiarioService : IApiariosService
    {
        private readonly HttpClient _http;
        public ApiarioService(HttpClient http) => _http = http;

        public Task<DashboardMetricas?> GetMetricasAsync()
        {
            throw new NotImplementedException("Backend no conectado. Usar DatosFicticios en el PageModel.");
        }

        public Task<UsuarioSimpleDto?> GetUsuarioAsync()
        {
            throw new NotImplementedException("Backend no conectado. Usar DatosFicticios en el PageModel.");
        }

        public async Task<ApiarioModel> RegistrarApiarioAsync(ApiarioCreateDto dto)
        {
            var resp = await _http.PostAsJsonAsync("api/Apiarios", dto);

            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Error creando apiario: " +
                                                    $"{(int)resp.StatusCode} {body}");
            }

            var created = await resp.Content.ReadFromJsonAsync<ApiarioModel>();
            if (created is null) throw new InvalidOperationException
                ("El backend no devolvió el apiario creado.");
            return created;
        }
    }
}