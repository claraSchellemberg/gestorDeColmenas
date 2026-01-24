using GestorDeColmenasFrontend.Dtos.Apiario;
using GestorDeColmenasFrontend.Dtos.Colmena;
using GestorDeColmenasFrontend.Dtos.Mediciones;
using GestorDeColmenasFrontend.Dtos.Registros;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Dev
{
    public static class DatosFicticios
    {
        // ID de usuario ficticio para desarrollo
        public const int UsuarioIdFicticio = 1;

        public static UsuarioSimpleDto GetUsuario() => new()
        {
            Id = UsuarioIdFicticio,
            Nombre = "Juan Pérez",
            Email = "juan.perez@example.com",
            FotoPerfil = "https://i.pravatar.cc/150?img=67"
        };
    }
}