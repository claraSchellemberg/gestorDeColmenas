using System.ComponentModel.DataAnnotations;

namespace GestorDeColmenasFrontend.Dtos.Apiario
{
    public class ApiarioCreateDto
    {
        [Required, StringLength(100)]
        public string? Nombre { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string? UbicacionDeReferencia { get; set; }
        public int UsuarioId { get; set; }
    }
}
