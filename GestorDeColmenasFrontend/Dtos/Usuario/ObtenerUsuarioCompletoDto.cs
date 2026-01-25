namespace GestorDeColmenasFrontend.Dtos.Usuario
{
    /// DTO para los datos del usuario que vienen dentro del token
    public class ObtenerUsuarioCompletoDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Contraseña { get; set; }
        public string? NumeroTelefono { get; set; }
        public string? NumeroApicultor { get; set; }
        public int MedioDeComunicacionDePreferencia { get; set; }
    }
}
