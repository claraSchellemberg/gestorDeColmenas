using GestorDeColmenasFrontend.Dtos.Usuario;

namespace GestorDeColmenasFrontend.Modelos
{
    public class DashboardViewModel
    {
        public DashboardMetricas Metricas { get; set; } = new DashboardMetricas();
        public UsuarioSimpleDto Usuario { get; set; } = new UsuarioSimpleDto();
    }
}
