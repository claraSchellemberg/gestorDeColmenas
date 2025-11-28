using GestorDeColmenasFrontend.Dtos;
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
            var metricas = new DashboardMetricas
            {
                Apiarios = 3,
                Colmenas = 24,
                BuenEstado = 21,
                Alertas = 3
            };
            return Task.FromResult<DashboardMetricas?>(metricas);
        }

        public Task<MapaViewModel?> GetMapaAsync()
        {
            var mapa = new MapaViewModel
            {
                // Usa cualquier imagen de fondo. Si no tienes, deja null y la cshtml usará el placeholder.
                Imagen = "https://via.placeholder.com/800x450",
                Puntos = new List<MapaPunto>
                {
                    new MapaPunto { Top = 30, Left = 20 },
                    new MapaPunto { Top = 55, Left = 62 },
                    new MapaPunto { Top = 78, Left = 35 }
                }
            };
            return Task.FromResult<MapaViewModel?>(mapa);
        }

        public Task<UsuarioViewModel?> GetUsuarioAsync()
        {
            var usuario = new UsuarioViewModel
            {
                // La vista solo usa FotoPerfil, así que no seteamos otros campos para evitar errores si no existen.
                FotoPerfil = "https://i.pravatar.cc/150?img=67"
            };
            return Task.FromResult<UsuarioViewModel?>(usuario);
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