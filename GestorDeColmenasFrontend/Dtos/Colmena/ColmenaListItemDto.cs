using GestorDeColmenasFrontend.Dtos.Mediciones;
using GestorDeColmenasFrontend.Extensions;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Dtos.Colmena
{
    public class ColmenaListItemDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? ApiarioNombre { get; set; }
        public int ApiarioId { get; set; }
        public MedicionColmenaDto? UltimaMedicion { get; set; }
        public List<CuadroModel> Cuadros { get; set; }
        public CondicionColmena? Estado { get; set; }
        public DateTime? FechaUltimaMedicion { get; set; }


        // Texto del estado para mostrar en la UI
        public string EstadoTexto => Estado.ToDisplayText();
    }
}
