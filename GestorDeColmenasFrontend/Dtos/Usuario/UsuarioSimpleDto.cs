using System.Runtime.ConstrainedExecution;

namespace GestorDeColmenasFrontend.Dtos.Usuario
{
    public class UsuarioSimpleDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? FotoPerfil { get; set; }
        //lo dejamos?... habria que agregarlo a la base y podria ser opcional
    }
}
