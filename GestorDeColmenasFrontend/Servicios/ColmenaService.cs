using GestorDeColmenasFrontend.Dtos.Colmena;
using GestorDeColmenasFrontend.Dtos.Mediciones;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Servicios
{
    public class ColmenaService : IColmenaService
    {
        private readonly HttpClient _http;
        private readonly ILogger<ColmenaService> _logger;
        public ColmenaService(HttpClient http, ILogger<ColmenaService> logger)
        {
            _http = http;
            _logger = logger;
        }
        public async Task<ColmenaDetalleDto?> GetColmenaDetalleAsync(int id)
        {
            try
            {
                var resp = await _http.GetAsync($"Colmenas/{id}/detalle");
                if (resp.IsSuccessStatusCode)
                {
                    var colmena = await resp.Content.ReadFromJsonAsync<ColmenaDetalleDto>();
                    return colmena;
                }
                else
                {
                    var errorMsg = await resp.Content.ReadAsStringAsync();
                    throw new InvalidOperationException($"Error obteniendo colmena: {(int)resp.StatusCode} {resp.ReasonPhrase}. {errorMsg}");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al obtener detalle de colmena {Id}", id);
                throw new InvalidOperationException("No se pudo conectar con el servidor. Verifique su conexión.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener detalle de colmena {Id}", id);
                throw new InvalidOperationException("Ocurrió un error inesperado al obtener la colmena.", ex);
            }
        }
        public async Task<List<ColmenaListItemDto>> GetColmenasAsync()
        {
            try
            {
                var resp = await _http.GetAsync("Colmenas");

                if (resp.IsSuccessStatusCode)
                {
                    var colmenas = await resp.Content.ReadFromJsonAsync<List<ColmenaListItemDto>>();
                    return colmenas ?? new List<ColmenaListItemDto>();
                }
                else
                {
                    var errorContent = await resp.Content.ReadAsStringAsync();
                    throw new InvalidOperationException($"Error obteniendo colmenas: {(int)resp.StatusCode} {resp.ReasonPhrase}. {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al obtener listado de colmenas");
                throw new InvalidOperationException("No se pudo conectar con el servidor. Verifique su conexión.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener listado de colmenas");
                throw new InvalidOperationException("Ocurrió un error inesperado al obtener las colmenas.", ex);
            }
        }

        public async Task<List<ColmenaModel>> GetColmenasPorApiarioAsync(int apiarioId)
        {
            try
            {
                var resp = await _http.GetAsync($"/Colmenas/apiario/{apiarioId}");
                
                if (resp.IsSuccessStatusCode)
                {
                    var colmenas = await resp.Content.ReadFromJsonAsync<List<ColmenaModel>>();
                    return colmenas ?? new List<ColmenaModel>();
                }
                else
                {
                    var errorContent = await resp.Content.ReadAsStringAsync();
                    throw new InvalidOperationException($"Error obteniendo colmenas: {(int)resp.StatusCode} {resp.ReasonPhrase}. {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al obtener colmenas por apiario {ApiarioId}", apiarioId);
                throw new InvalidOperationException("No se pudo conectar con el servidor. Verifique su conexión.", ex);
            }
            catch (InvalidOperationException)
            {
                throw; // Re-throw business exceptions as-is
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener colmenas por apiario {ApiarioId}", apiarioId);
                throw new InvalidOperationException("Ocurrió un error inesperado al obtener las colmenas del apiario.", ex);
            }
        }

        public Task<List<RegistroMedicionDto>> GetHistorialMedicionesAsync(int colmenaId, int pagina = 1, int registrosPorPagina = 10)
        {
            throw new NotImplementedException("Backend no conectado. Usar DatosFicticios en el PageModel.");
        }
        public async Task<ColmenaModel> RegistrarAsync(int apiarioId, ColmenaCreateDto dto)
        {
            try
            {
                var respuesta = await _http.PostAsJsonAsync($"/Colmenas", dto);
                if (!respuesta.IsSuccessStatusCode)
                {
                    var msj = await respuesta.Content.ReadAsStringAsync();
                    throw new InvalidOperationException($"Error al registrar colmena: {(int)respuesta.StatusCode} {msj}");
                }

                var colmenaCreada = await respuesta.Content.ReadFromJsonAsync<ColmenaModel>();
                if (colmenaCreada is null)
                {
                    throw new InvalidOperationException("El backend no devolvió la colmena creada.");
                }

                return colmenaCreada;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al registrar colmena");
                throw new InvalidOperationException("No se pudo conectar con el servidor. Verifique su conexión.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al registrar colmena");
                throw new InvalidOperationException("Ocurrió un error inesperado al registrar la colmena.", ex);
            }
        }
    }
}