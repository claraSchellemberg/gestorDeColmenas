using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Extensions
{
    /// <summary>
    /// Extensiones para el enum EstadoColmena
    /// Centraliza la lógica de conversión a texto (DRY principle)
    /// </summary>
    public static class EstadoColmenaExtensions
    {
        public static string ToDisplayText(this EstadoColmena? estado)
        {
            return estado switch
            {
                EstadoColmena.OPTIMO => "Saludable",
                EstadoColmena.NECESITA_REVISION => "Observación",
                EstadoColmena.EN_PELIGRO => "Alerta",
                _ => "Sin datos"
            };
        }

        public static (string BgColor, string TextColor, string DotColor) ToStatusColors(this EstadoColmena? estado)
        {
            return estado switch
            {
                EstadoColmena.OPTIMO => ("bg-green-100 dark:bg-green-900/50", "text-accent-green dark:text-green-300", "bg-accent-green"),
                EstadoColmena.NECESITA_REVISION => ("bg-orange-100 dark:bg-orange-900/50", "text-accent-orange dark:text-orange-300", "bg-accent-orange"),
                EstadoColmena.EN_PELIGRO => ("bg-red-100 dark:bg-red-900/50", "text-accent-red dark:text-red-300", "bg-accent-red"),
                _ => ("bg-gray-200 dark:bg-gray-600", "text-gray-800 dark:text-gray-300", "bg-gray-500")
            };
        }
    }
}