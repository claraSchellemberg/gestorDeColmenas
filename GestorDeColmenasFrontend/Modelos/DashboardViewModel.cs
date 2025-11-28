namespace GestorDeColmenasFrontend.Modelos
{
    public class DashboardViewModel
    {
        public DashboardMetricas Metricas { get; set; } = new DashboardMetricas();
        public MapaViewModel Mapa { get; set; } = new MapaViewModel();
        public UsuarioViewModel Usuario { get; set; } = new UsuarioViewModel();
    }
}
