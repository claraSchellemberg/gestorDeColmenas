using GestorDeColmenasFrontend.Extensions;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Dtos.Mediciones
{
    public class RegistroMedicionDto
    {
        public int Id { get; set; }
        public DateTime FechaMedicion { get; set; }
        public float TempInterna1 { get; set; }
        public float TempInterna2 { get; set; }
        public float TempInterna3 { get; set; }
        public float TempExterna { get; set; }
        public float Peso { get; set; }
        public EstadoColmena? Estado { get; set; }
        public string? MensajeEstado { get; set; }
        public string EstadoTexto => Estado.ToDisplayText();
    }
}
