namespace GestorDeColmenasFrontend.Modelos
{
    public class MapaPunto
    {
        // valores en porcentaje (0..100)
        public double Top { get; set; }
        public double Left { get; set; }
        // Podés añadir campos como Tooltip, Estado, IdApiario, etc.
        public string Tooltip { get; set; }
        public string Estado { get; set; }
    }
}
