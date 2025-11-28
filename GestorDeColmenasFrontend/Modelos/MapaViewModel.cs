namespace GestorDeColmenasFrontend.Modelos
{
    public class MapaViewModel
    {
        // URL de la imagen de fondo (o usá un servicio de mapas si querés)
        public string Imagen { get; set; } = "https://via.placeholder.com/800x450";

        // Puntos en porcentaje (top, left) para posicionar los pines en la imagen
        public List<MapaPunto> Puntos { get; set; } = new List<MapaPunto>();
    }
}
