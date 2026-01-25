namespace GestorDeColmenasFrontend.Modelos
{
    public class RegistroUsuarioModel
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Contraseña { get; set; }
        public string? NumeroTelefono { get; set; }
        public string? NumeroApicultor { get; set; }
        public CanalPreferidoNotificacion MedioDeComunicacionDePreferencia { get; set; }
        public string? FotoPerfil { get; set; } // ← VERIFICAR QUE ESTÉ

    }
}
