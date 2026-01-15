using Microsoft.Win32;

namespace GestorDeColmenasFrontend.Modelos
{
    public class NotificacionModel
    {
        public int Id { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public DateTime FechaNotificacion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int? RegistroAsociadoId { get; set; }
        public bool EsNoLeida => Estado == "ENVIADA";
    }
}
