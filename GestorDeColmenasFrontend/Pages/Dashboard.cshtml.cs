using GestorDeColmenasFrontend.Dev;
using GestorDeColmenasFrontend.Dtos.Apiario;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IApiariosService _apiariosService;
        
        public DashboardModel(IApiariosService apiariosService)
        {
            _apiariosService = apiariosService;
        }

        public DashboardViewModel ViewModel { get; set; } = new();
        
        [BindProperty] 
        public ApiarioCreateDto NuevoApiarioDto { get; set; } = new();

        public void OnGet()
        {
            ViewModel = new DashboardViewModel
            {
                Metricas = DatosFicticios.GetMetricas(),
                Usuario = DatosFicticios.GetUsuario()
            };
        }

        public async Task<IActionResult> OnPostAgregarApiarioAsync()
        {
            if (!ModelState.IsValid)
            {
                OnGet();
                TempData["OpenApiarioModal"] = true;
                return Page();
            }

            try
            {
                var creado = await _apiariosService.RegistrarApiarioAsync(NuevoApiarioDto);
                TempData["ToastSuccess"] = $"Apiario '{creado.Nombre}' creado (Id {creado.Id}).";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                OnGet();
                TempData["OpenApiarioModal"] = true;
                return Page();
            }
        }
    }
}
