using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Helpers;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages
{
    public class PerfilUsuarioModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<PerfilUsuarioModel> _logger;

        public PerfilUsuarioModel(IUsuarioService usuarioService, ILogger<PerfilUsuarioModel> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [BindProperty]
        public PerfilUsuarioDto Usuario { get; set; } = new();

        [BindProperty]
        public string? NuevaContrasena { get; set; }

        [BindProperty]
        public string? ConfirmarContrasena { get; set; }

        public string MensajeError { get; set; } = string.Empty;
        public string MensajeExito { get; set; } = string.Empty;

        // Obtener y mostrar el perfil del usuario logueado
        public async Task OnGetAsync()
        {
            try
            {
                int usuarioId = SessionHelper.GetUsuarioIdOrDefault(HttpContext.Session);

                var perfilDto = await _usuarioService.GetPerfilAsync(usuarioId);
                if (perfilDto is not null)
                {
                    // Uso del mapper: asigna directamente a la propiedad binded
                    Usuario = UsuarioMapper.ToPerfilUsuarioDto(perfilDto);
                }
                else
                {
                    _logger.LogWarning("No se obtuvo perfil del backend para UsuarioId={UsuarioId}", usuarioId);
                    MensajeError = "No se pudieron cargar los datos de perfil.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar perfil del usuario");
                MensajeError = "Error al cargar el perfil. Por favor intenta nuevamente.";
            }
        }

        // Handler para guardar cambios del perfil (asp-page-handler="GuardarPerfil")
        public async Task<IActionResult> OnPostGuardarPerfilAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Validación de cambio de contraseña
            if (!string.IsNullOrEmpty(NuevaContrasena))
            {
                if (NuevaContrasena != ConfirmarContrasena)
                {
                    ModelState.AddModelError(nameof(ConfirmarContrasena), "Las contraseñas no coinciden");
                    return Page();
                }
            }

            try
            {
                int usuarioId = SessionHelper.GetUsuarioIdOrDefault(HttpContext.Session);

                // Mapear a DTO que espera el servicio usando el mapper
                var dto = UsuarioMapper.ToUsuarioCreateDto(Usuario);
                if (!string.IsNullOrEmpty(NuevaContrasena))
                {
                    dto.Contraseña = NuevaContrasena;
                }

                var actualizado = await _usuarioService.ActualizarPerfilAsync(usuarioId, dto);
                if (actualizado)
                {
                    _logger.LogInformation("Perfil del usuario {UsuarioId} actualizado correctamente", usuarioId);
                    TempData["ToastSuccess"] = "Perfil actualizado correctamente";
                    return RedirectToPage(); // recarga para mostrar datos actualizados
                }
                else
                {
                    _logger.LogWarning("El backend respondió sin éxito al actualizar perfil UsuarioId={UsuarioId}", usuarioId);
                    MensajeError = "No se pudieron guardar los cambios. Por favor intenta nuevamente.";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar perfil del usuario");
                MensajeError = "Ocurrió un error al guardar. Por favor intenta nuevamente.";
                return Page();
            }
        }
    }
}