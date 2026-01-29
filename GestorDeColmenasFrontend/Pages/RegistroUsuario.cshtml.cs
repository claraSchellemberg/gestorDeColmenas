using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Helpers;
using GestorDeColmenasFrontend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GestorDeColmenasFrontend.Pages
{
    public class RegistroUsuarioModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<RegistroUsuarioModel> _logger;

        public RegistroUsuarioModel(IUsuarioService usuarioService, ILogger<RegistroUsuarioModel> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [BindProperty]
        public UsuarioCreateDto NuevoUsuario { get; set; } = new UsuarioCreateDto();

        public string MensajeExito { get; set; } = string.Empty;
        public string MensajeError { get; set; } = string.Empty;

        public void OnGet()
        {
            NuevoUsuario = new UsuarioCreateDto();
            MensajeExito = string.Empty;
            MensajeError = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                MensajeError = "Algunos campos no son válidos.";
                return Page();          
            }

            try
            {
                var usuarioCreado = await _usuarioService.RegistrarUsuarioAsync(NuevoUsuario);

                if (usuarioCreado == null || usuarioCreado.Id <= 0)
                {
                    _logger.LogError("Registro correcto pero no se devolvió Id válido. Email: {Email}", NuevoUsuario.Email);
                    MensajeError = "Registro completado, pero no se pudo iniciar sesión automáticamente. Por favor inicia sesión manualmente.";
                    return Page();
                }

                // Guardar sesión y redirige al dashboard
                SessionHelper.SetUsuarioId(HttpContext.Session, usuarioCreado.Id);
                HttpContext.Session.SetString("UsuarioCorreo", usuarioCreado.Email ?? string.Empty);
                HttpContext.Session.SetString("UsuarioNombre", usuarioCreado.Nombre ?? string.Empty);
                _logger.LogInformation("Usuario registrado e inició sesión automáticamente: {Email}", usuarioCreado.Email);
                return RedirectToPage("/Dashboard");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Error al registrar usuario: {Message}", ex.Message);
                MensajeError = $"No se pudo registrar el usuario: {ex.Message}";
                return Page();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al registrar usuario");
                MensajeError = "Error de conexión con el servidor. Por favor intenta más tarde.";
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al registrar usuario");
                MensajeError = "Ocurrió un error inesperado. Por favor intenta nuevamente.";
                return Page();
            }
        }
    }
}
