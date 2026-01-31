using GestorDeColmenasFrontend.Dtos;
using GestorDeColmenasFrontend.Dtos.Apiario;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Modelos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestorDeColmenasFrontend.Servicios
{
    public class ApiarioService : IApiariosService
    {
        
        private readonly HttpClient _http;
        //agrego cambios para que traiga los apiarios solo para ese usuario
        private readonly ILogger<ApiarioService> _logger;

        public ApiarioService(HttpClient http, ILogger<ApiarioService> logger)
        {
            _http = http;
            _logger = logger;
        } //=> _http = http;

        public Task<ApiarioModel> GetApiarioPorNombreYUsuario(string nombre, int usuarioId)
        {
            var resp = _http.GetAsync($"Apiarios/nombre/{nombre}/usuario/{usuarioId}");
            if(resp.Result.IsSuccessStatusCode)
            {
                var apiario = resp.Result.Content.ReadFromJsonAsync<ApiarioModel>();
                return apiario!;
            }
            else
            {
                throw new InvalidOperationException($"Error obteniendo apiario: {(int)resp.Result.StatusCode} {resp.Result.ReasonPhrase}");
            }
        }
        
        //agrego cambios para que traiga so apiarios solo por el id del usuario
        public async Task<List<ApiarioModel>> GetApiarios(int usuarioId)
        {
            try
            {
                var resp = await _http.GetAsync($"Apiarios/usuario/{usuarioId}");
                if (resp.IsSuccessStatusCode)
                {
                    var apiarios = await resp.Content.ReadFromJsonAsync<List<ApiarioModel>>();
                    _logger.LogInformation("Se obtuvieron {Count} apiarios para el usuario {usuarioId}", apiarios?.Count?? 0, usuarioId);
                    return apiarios ?? new List<ApiarioModel>();
                }
                else
                {
                    var errorContent = await resp.Content.ReadAsStringAsync();
                    _logger.LogWarning("Error obteniendo apiarios para el usuario {usuarioId}: {StatusCode}- {Error}", usuarioId, resp.StatusCode, errorContent);
                    throw new InvalidOperationException($"Error obteniendo apiarios: {(int)resp.StatusCode} {resp.ReasonPhrase}");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al obtener apiarios del usuario {usuarioId}", usuarioId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener apiarios del usuario {usuarioId}", usuarioId);
                throw;
            }
        }

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
            var resp = await _http.PostAsJsonAsync("Apiarios", dto);

            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Error creando apiario: {(int)resp.StatusCode} {body}");
            }
            var apiarioCreado = await resp.Content.ReadFromJsonAsync<ApiarioModel>();
            if(apiarioCreado is null)
            {
                throw new InvalidOperationException("El backend no devolvió el apiario creado.");
            }
            return apiarioCreado;
        }

        public async Task<ServiceResult> EliminarApiarioAsync(int id)
        {
            try
            {
                var resp = await _http.DeleteAsync($"Apiarios?id={id}");

                if (resp.IsSuccessStatusCode)
                {
                    return ServiceResult.Ok();
                }

                var body = await resp.Content.ReadAsStringAsync();
                return ServiceResult.Fail($"Error {(int)resp.StatusCode}: {body}");
            }
            catch (HttpRequestException ex)
            {
                return ServiceResult.Fail($"Error de conexión: {ex.Message}");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail($"Error inesperado: {ex.Message}");
            }
        }
    }
}