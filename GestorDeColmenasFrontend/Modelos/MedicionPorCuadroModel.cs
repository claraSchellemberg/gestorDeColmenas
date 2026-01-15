namespace GestorDeColmenasFrontend.Modelos
{
    public class MedicionPorCuadroModel
    {
        public float TempInterna1 { get; set; }
        public float TempInterna2 { get; set; }
        public float TempInterna3 { get; set; }
        public DateTime FechaMedicion { get; set; }
        public CuadroModel Cuadro { get; set; }
        public float PromedioTemperatura => (TempInterna1 + TempInterna2 + TempInterna3) / 3;
    }
}