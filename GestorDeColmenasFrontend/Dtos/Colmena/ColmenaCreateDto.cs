using System.ComponentModel.DataAnnotations;

namespace GestorDeColmenasFrontend.Dtos.Colmena
{
    public class ColmenaCreateDto
    {
        [Required, StringLength(100)]
        public string Nombre { get; set; }

        [Required, StringLength(100)]
        public string Descripcion { get; set; }
        public int? ApiarioId { get; set; }
        public int CantidadCuadros { get; set; } = 1; // Por defecto 1 cuadro
    }
}
