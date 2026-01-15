namespace GestorDeColmenasFrontend.Modelos
{
    public class CuadroModel
    {
        public ColmenaModel Colmena { get; set; }
        public List<MedicionPorCuadroModel> Mediciones { get; set; }
        public MedicionPorCuadroModel? UltimaMedicion =>
           Mediciones?.OrderByDescending(m => m.FechaMedicion).FirstOrDefault();

    }
}
