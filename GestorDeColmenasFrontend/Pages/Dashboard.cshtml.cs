using GestorDeColmenasFrontend.Dev;
using GestorDeColmenasFrontend.Dtos.Apiario;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Helpers;
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
        public List<ApiarioModel> Apiarios { get; set; } = new();

        [BindProperty] 
        public ApiarioCreateDto NuevoApiarioDto { get; set; } = new();

        public async Task OnGetAsync()
        {
            ViewModel = new DashboardViewModel
            {
                Metricas = DatosFicticios.GetMetricas(),
                Usuario = DatosFicticios.GetUsuario()
            };
            //cargamos los apiarios en el mapa
            Apiarios = await _apiariosService.GetApiarios();
        }

        public async Task<IActionResult> OnPostAgregarApiarioAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                TempData["OpenApiarioModal"] = true;
                return Page();
            }

            try
            {
                // setea el id del usuario desde la session (o id falso por defecto)
                NuevoApiarioDto.UsuarioId = SessionHelper.GetUsuarioIdOrDefault(HttpContext.Session);
                
                await _apiariosService.RegistrarApiarioAsync(NuevoApiarioDto);
                TempData["ToastSuccess"] = $"Apiario '{NuevoApiarioDto.Nombre}' creado correctamente.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await OnGetAsync();
                TempData["OpenApiarioModal"] = true;
                return Page();
            }
        }
    }
}
