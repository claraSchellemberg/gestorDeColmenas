namespace GestorDeColmenasFrontend.Modelos
{
    /// <summary>
    /// Representa un registro/evento de la colmena (inspecciones, observaciones, etc.)
    /// Las mediciones de temperatura y peso ahora están en MedicionesPorColmenaModel y MedicionPorCuadroModel
    /// </summary>
    public class RegistroModel
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string? Descripcion { get; set; }
        public string? TipoRegistro { get; set; } // Ej: "Inspección", "Observación", "Tratamiento"
        public ColmenaModel? Colmena { get; set; }
    }
}