using GestorDeColmenasFrontend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;
        private readonly IAuthService _authService;
        private readonly ISession session;

        public LogoutModel(ILogger<LogoutModel> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _authService.LogoutAsync();
                _logger.LogInformation("User logged out successfully.");
                return RedirectToPage("/LoginUsuario");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during logout.");
                TempData["ToastError"] = "Ocurrió un error al cerrar sesión. Por favor, inténtelo de nuevo.";
                return RedirectToPage("/Index");
            }
        }
    }
}
