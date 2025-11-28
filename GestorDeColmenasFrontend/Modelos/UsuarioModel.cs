namespace GestorDeColmenasFrontend.Modelos
{
    public class UsuarioModel
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public List<ApiarioModel> Apiarios { get; set; } = new List<ApiarioModel>();
    }
}
