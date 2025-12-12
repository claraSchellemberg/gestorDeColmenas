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
        public float TempExterna { get; set; }
        public float Peso { get; set; }
        public List<MedicionCuadroDto> MedicionesCuadros { get; set; } = new();
        public EstadoColmena? Estado { get; set; }
        public DateTime? FechaUltimaMedicion { get; set; }

        // Promedio de temperatura interna 1 de todos los cuadros
        public float? PromedioTempInterna1 =>
            MedicionesCuadros.Any() ? MedicionesCuadros.Average(m => m.TempInterna1) : null;

        // Promedio de temperatura interna 2 de todos los cuadros
        public float? PromedioTempInterna2 =>
            MedicionesCuadros.Any() ? MedicionesCuadros.Average(m => m.TempInterna2) : null;

        // Promedio de temperatura interna 3 de todos los cuadros
        public float? PromedioTempInterna3 =>
            MedicionesCuadros.Any() ? MedicionesCuadros.Average(m => m.TempInterna3) : null;

        // Texto del estado para mostrar en la UI
        public string EstadoTexto => Estado.ToDisplayText();
    }
}
