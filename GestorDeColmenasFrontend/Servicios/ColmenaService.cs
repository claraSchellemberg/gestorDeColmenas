using GestorDeColmenasFrontend.Dtos;
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

        public async Task<ColmenaModel> RegistrarAsync(int apiarioId, ColmenaCreateDto dto)
        {
            var respuesta = await _http.PostAsJsonAsync($"api/apiarios/{apiarioId}/colmenas", dto);
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
