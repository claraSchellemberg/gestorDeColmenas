using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Helpers;

namespace GestorDeColmenasFrontend.Pages
{
    public class LoginUsuarioModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<LoginUsuarioModel> _logger;

        public LoginUsuarioModel(IUsuarioService usuarioService, ILogger<LoginUsuarioModel> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [BindProperty]
        public string Correo { get; set; } = string.Empty;

        [BindProperty]
        public string Contraseña { get; set; } = string.Empty;

        public string MensajeError { get; set; } = string.Empty;

        public void OnGet()
        {
            // Limpiar sesión si el usuario ya estaba logueado
            if (HttpContext.Session.Keys.Contains("UsuarioId"))
            {
                HttpContext.Session.Clear();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                if (string.IsNullOrWhiteSpace(Correo) || string.IsNullOrWhiteSpace(Contraseña))
                {
                    MensajeError = "Por favor ingresa tu correo electrónico y contraseña.";
                    return Page();
                }

                // se crea el DTO con credenciales
                var credenciales = new LoginUsuarioDto
                {
                    Email = Correo,
                    Contraseña = Contraseña
                };

                // Autenticar con el backend
                var usuario = await _usuarioService.AutenticarAsync(credenciales);

                // Validar que la autenticación fue exitosa
                if (usuario == null)
                {
                    MensajeError = "Correo o contraseña incorrectos.";
                    _logger.LogWarning("Intento de inicio de sesión fallido para el correo: {Email}", Correo);
                    return Page();
                }

                // Validar que el usuario tenga un ID válido
                if (usuario.Id <= 0)
                {
                    MensajeError = "Error: El usuario no tiene un ID válido.";
                    _logger.LogError("Usuario autenticado pero sin ID válido: {Email}", usuario.Email);
                    return Page();
                }

                // se guarda la sesión del usuario
                SessionHelper.SetUsuarioId(HttpContext.Session, usuario.Id);
                HttpContext.Session.SetString("UsuarioCorreo", usuario.Email ?? string.Empty);
                HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre ?? string.Empty);

                _logger.LogInformation("Usuario {Email} inició sesión exitosamente.", usuario.Email);

                return RedirectToPage("/Dashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al autenticar al usuario con correo: {Email}", Correo);
                MensajeError = "Error al iniciar sesión. Por favor intenta nuevamente.";
                return Page();
            }
        }
    }
}
