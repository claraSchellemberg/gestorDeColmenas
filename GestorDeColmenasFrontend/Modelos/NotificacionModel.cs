using Microsoft.Win32;

namespace GestorDeColmenasFrontend.Modelos
{
    public class NotificacionModel
    {
        public string Mensaje { get; set; }
        public DateTime FechaNotificacion { get; set; }
        public RegistroModel RegistroAsociado { get; set; }
    }
}
