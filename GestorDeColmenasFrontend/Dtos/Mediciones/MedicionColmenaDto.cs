using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Dtos.Mediciones
{
    public class MedicionColmenaDto
    {
        public ColmenaModel Colmena { get; set; }
        public float? Peso { get; set; }
        public float? TempExterna { get; set; }
    }
}
