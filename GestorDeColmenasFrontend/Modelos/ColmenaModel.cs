using Microsoft.Win32;

namespace GestorDeColmenasFrontend.Modelos
{
    public class ColmenaModel
    {
        public string Nombre { get; set; }
        public DateTime FechaInstalacionSensores { get; set; }
        public string Descripcion { get; set; }
        public EstadoColmena Estado { get; set; }
        public List<RegistroModel> Registros { get; set; } = new List<RegistroModel>();
    }
}