namespace GestorDeColmenasFrontend.Dtos.Apiario
{
    public class ApiarioListItemDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string UbicacionDeReferencia { get; set; } = string.Empty;
        public int CantidadColmenas { get; set; }
    }
}
