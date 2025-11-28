using System.ComponentModel.DataAnnotations;

namespace GestorDeColmenasFrontend.Dtos
{
    public class ApiarioCreateDto
    {
        [Required, StringLength(100)]
        public string? Nombre { get; set; }

        [Range(-90, 90)]
        public string? Latitud { get; set; }

        [Range(-180, 180)]
        public string? Longitud { get; set; }

        public string? UbicacionDeReferencia { get; set; }

    }
}
