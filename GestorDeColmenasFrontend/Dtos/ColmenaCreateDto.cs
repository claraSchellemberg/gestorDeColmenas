using System.ComponentModel.DataAnnotations;

namespace GestorDeColmenasFrontend.Dtos
{
    public class ColmenaCreateDto
    {
        [Required, StringLength(100)]
        public string Nombre { get; set; }

        [Required, StringLength(100)]
        public string Descripcion { get; set; }
    }
}
