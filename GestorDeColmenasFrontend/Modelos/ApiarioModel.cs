namespace GestorDeColmenasFrontend.Modelos
{
    public class ApiarioModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string? UbicacionDeReferencia { get; set; }
        public DateTime FechaAlta { get; set; } = DateTime.Now;
        public List<ColmenaModel>? Colmenas { get; set; }
        public bool HayColmenaEnPeligro { get; set; }
    }
}
