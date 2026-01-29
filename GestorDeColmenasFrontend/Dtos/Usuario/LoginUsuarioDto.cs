using System.ComponentModel.DataAnnotations;

namespace GestorDeColmenasFrontend.Dtos.Usuario
{
    public class LoginUsuarioDto
    {
        public string? Email { get; set; }
        
        [DataType(DataType.Password)]
        public string? Contraseña { get; set; }
    }
}
