using Microsoft.Win32;

namespace GestorDeColmenasFrontend.Modelos
{
    public class ColmenaModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaInstalacionSensores { get; set; }
        public string? Descripcion { get; set; }
        public EstadoColmena Estado { get; set; }
        
        // Relaciones
        public ApiarioModel? Apiario { get; set; }
        public List<CuadroModel> Cuadros { get; set; } = new();
        public List<MedicionesPorColmenaModel> Mediciones { get; set; } = new();
        public List<RegistroModel> Registros { get; set; } = new(); // Ahora son eventos/observaciones
        //Ultimas mediciones de colmena
        public MedicionesPorColmenaModel? UltimaMedicion => 
            Mediciones?.OrderByDescending(m => m.FechaMedicion).FirstOrDefault();

        // últimas mediciones de todos los cuadros
        public List<MedicionPorCuadroModel> UltimasMedicionesCuadros =>
            Cuadros?.Select(c => c.UltimaMedicion).Where(m => m != null).ToList()! ?? new();
    }
}