using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Dtos.Usuario
{
    public class UsuarioCreateDto
    {
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Contraseña { get; set; }
        public string? NumeroTelefono { get; set; }
        public string? NumeroApicultor { get; set; }
        public CanalPreferidoNotificacion MedioDeComunicacionDePreferencia { get; set; }
       public string? FotoPerfil { get; set; }
    }
}
