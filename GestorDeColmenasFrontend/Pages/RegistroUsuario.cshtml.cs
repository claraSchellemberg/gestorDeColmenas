using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Helpers;
using GestorDeColmenasFrontend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages
{
    public class RegistroUsuarioModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IApiariosService _apiariosService;
        private readonly IColmenaService _colmenaService;
        private readonly ILogger<PerfilUsuarioModel> _logger;

        public RegistroUsuarioModel(IUsuarioService usuarioService, IApiariosService apiariosService, IColmenaService colmenaService, ILogger<PerfilUsuarioModel> logger)
        {
            _usuarioService = usuarioService;
            _apiariosService = apiariosService;
            _colmenaService = colmenaService;
            _logger = logger;
        }

        [BindProperty]
        public UsuarioCreateDto Perfil { get; set; } = new UsuarioCreateDto();

        public string MensajeExito { get; set; } = string.Empty;
        public string MensajeError { get; set; } = string.Empty;
        public int ApiariosCuenta { get; set; }
        public int ColmenasCuenta { get; set; }
        public DateTime? UltimaActividad { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                int usuarioId = SessionHelper.GetUsuarioIdOrDefault(HttpContext.Session);

                // TODO: Reemplazar con datos reales del backend
                // var usuarioDto = await _usuarioService.GetUsuarioActualAsync();

                // TODO: Obtener perfil real del backend
                var perfil = await _usuarioService.GetPerfilAsync(usuarioId);
                if (perfil != null)
                {
                    Perfil = perfil;
                }
                else
                {
                    // Datos ficticios si el backend no está disponible
                    Perfil = new UsuarioCreateDto
                    {
                        Nombre = "Juan",
                        Email = "juan@ejemplo.com",
                        NumeroTelefono = "991234567",
                        NumeroApicultor = "AR-2024-0001",
                        MedioDeComunicacionDePreferencia = Modelos.CanalPreferidoNotificacion.EMAIL
                    };
                }
                // Por ahora, usar datos ficticios

                // Obtener estadísticas
                try
                {
                    var apiarios = await _apiariosService.GetApiarios(usuarioId);
                    ApiariosCuenta = apiarios?.Count ?? 0;
                }
                catch
                {
                    ApiariosCuenta = 0;
                }
                // TODO: Contar colmenas reales
                ColmenasCuenta = 0;
                UltimaActividad = DateTime.Now.AddHours(-2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar el perfil");
                MensajeError = "Error al cargar el perfil. Por favor intenta nuevamente.";
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
                // Validar que los campos requeridos estén completos
                if (string.IsNullOrWhiteSpace(Perfil.Nombre) || string.IsNullOrWhiteSpace(Perfil.Email))
                {
                    MensajeError = "Por favor completa todos los campos requeridos.";
                    return Page();
                }
                int usuarioId = SessionHelper.GetUsuarioIdOrDefault(HttpContext.Session);

                // TODO: Implementar actualización en el backend
                // await _usuarioService.ActualizarPerfilAsync(usuarioId, Perfil);

                //MensajeExito = "Perfil actualizado exitosamente.";
                //await OnGetAsync();
                //return Page();
                // TODO: Actualizar perfil en el backend
                bool actualizado = await _usuarioService.ActualizarPerfilAsync(usuarioId, Perfil);

                if (actualizado)
                {
                    MensajeExito = "Perfil actualizado exitosamente.";
                    _logger.LogInformation($"Perfil del usuario {usuarioId} actualizado");
                }
                else
                {
                    MensajeError = "No se pudo actualizar el perfil. Por favor intenta nuevamente.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el perfil");
                MensajeError = "Error al actualizar el perfil. Por favor intenta nuevamente.";
                //return Page();
            }
            await OnGetAsync();
            return Page();
        }
    }
}
