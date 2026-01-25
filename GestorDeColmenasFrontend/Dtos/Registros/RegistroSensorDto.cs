namespace GestorDeColmenasFrontend.Dtos.Registros
{
    public class RegistroSensorGetDto : RegistroGetDto
    {
        public int SensorPorCuadroId { get; set; }
        //public float TempInterna1 { get; set; }
        //public float TempInterna2 { get; set; }
        //public float TempInterna3 { get; set; }
        //public DateTime FechaMedicion { get; set; }
        public int SensorId { get; set; }
        public int CuadroId { get; set; }
        public string? TipoSensor { get; set; }
        // borramos TempInterna1/2/3 y FechaMedicion del DTO que heredan.
        // en vez d eso, usamos las propiedades de la clase base (RegistroGetDto)
        // (TempInterna1/2/3,
        // FechaMedicion).

    }
}
