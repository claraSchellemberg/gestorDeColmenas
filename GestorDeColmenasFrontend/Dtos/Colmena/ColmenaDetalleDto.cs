using GestorDeColmenasFrontend.Extensions;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Dtos.Colmena
{
    public class ColmenaDetalleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? ApiarioNombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaInstalacionSensores { get; set; }
        public CondicionColmena? Estado { get; set; }
        public int CantidadCuadros { get; set; }
        public int CantidadRegistros { get; set; }

        // Métricas actuales
        public float TempInterna1 { get; set; }
        public float TempInterna2 { get; set; }
        public float TempInterna3 { get; set; }
        public float TempExterna { get; set; }
        public float Peso { get; set; }
        public string EstadoTexto => Estado.ToDisplayText();
    }
}
