using GestorDeColmenasFrontend.Dtos.Colmena;
using GestorDeColmenasFrontend.Dtos.Mediciones;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Servicios
{
    public class ColmenaService : IColmenaService
    {
        private readonly HttpClient _http;
        public ColmenaService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ColmenaDetalleDto?> GetColmenaDetalleAsync(int id)
        {
            var resp = await _http.GetAsync($"Colmenas/{id}/detalle");
            if (resp.IsSuccessStatusCode)
            {
                var colmena = await resp.Content.ReadFromJsonAsync<ColmenaDetalleDto>();
                return colmena;
            }
            else
            {
                throw new InvalidOperationException($"Error obteniendo colmena: {(int)resp.StatusCode} {resp.ReasonPhrase}");
            }

        }
        public async Task<List<ColmenaListItemDto>> GetColmenasAsync()
        {
            var resp = await _http.GetAsync("Colmenas");

            if (resp.IsSuccessStatusCode)
            {
                var colmenas = await resp.Content.ReadFromJsonAsync<List<ColmenaListItemDto>>();
                return colmenas ?? new List<ColmenaListItemDto>();
            }
            else
            {
                throw new InvalidOperationException($"Error obteniendo apiarios: {(int)resp.StatusCode} {resp.ReasonPhrase}");
            }
        }
        public Task<List<RegistroMedicionDto>> GetHistorialMedicionesAsync(int colmenaId, int pagina = 1, int registrosPorPagina = 10)
        {
            throw new NotImplementedException("Backend no conectado. Usar DatosFicticios en el PageModel.");
        }
        public async Task<ColmenaModel> RegistrarAsync(int apiarioId, ColmenaCreateDto dto)
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
    }
}