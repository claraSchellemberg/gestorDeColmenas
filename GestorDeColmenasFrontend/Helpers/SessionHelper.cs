namespace GestorDeColmenasFrontend.Helpers
{
    public static class SessionHelper
    {
        private const string UsuarioIdKey = "UsuarioId";

        public static void SetUsuarioId(ISession session, int usuarioId)
        {
            session.SetInt32(UsuarioIdKey, usuarioId);
        }

        public static int? GetUsuarioId(ISession session)
        {
            return session.GetInt32(UsuarioIdKey);
        }

        /// <summary>
        /// Gets the user ID or returns the fake ID for development.
        /// Replace this with proper authentication later.
        /// </summary>
        public static int GetUsuarioIdOrDefault(ISession session)
        {
            return session.GetInt32(UsuarioIdKey) ?? Dev.DatosFicticios.UsuarioIdFicticio;
        }
    }
}