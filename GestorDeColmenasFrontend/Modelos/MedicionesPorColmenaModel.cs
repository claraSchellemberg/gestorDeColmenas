namespace GestorDeColmenasFrontend.Modelos
{
    /// <summary>
    /// Mediciones del sensor de temperatura externa y peso de la colmena
    /// </summary>
    public class MedicionesPorColmenaModel
    {
        public int Id { get; set; }
        public float TempExterna { get; set; }
        public float Peso { get; set; }
        public DateTime FechaMedicion { get; set; }
        public ColmenaModel Colmena { get; set; }
    }
}