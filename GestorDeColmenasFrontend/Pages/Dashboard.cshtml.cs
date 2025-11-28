using GestorDeColmenasFrontend.Dtos;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly IApiariosService _apiariosService;
        private readonly IColmenaService _colmenaService;
        
        public DashboardModel(IApiariosService apiariosService, IColmenaService colmenaService)
        {
            _apiariosService = apiariosService;
            _colmenaService = colmenaService;
        }
        public DashboardViewModel ViewModel { get; set; } = new();
        [BindProperty] public ApiarioCreateDto NuevoApiarioDto { get; set; } = new();

        public async Task OnGetAsync()
        {
            var metricas = await _apiariosService.GetMetricasAsync();
            var mapa = await _apiariosService.GetMapaAsync();
            var usuario = await _apiariosService.GetUsuarioAsync();

            ViewModel = new DashboardViewModel
            {
                Metricas = metricas ?? new DashboardMetricas(),
                Mapa = mapa ?? new MapaViewModel(),
                Usuario = usuario ?? new UsuarioViewModel()
            };
        }

        public async Task<IActionResult> OnPostAgregarApiarioAsync()
        {
            Console.WriteLine("a ver si entro aca");
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // recargar datos para re-renderizar la página
                TempData["OpenApiarioModal"] = true; // reabrir modal con errores
                return Page();
            }
            try
            {
                var creado = await _apiariosService.RegistrarApiarioAsync(NuevoApiarioDto);

                TempData["ToastSuccess"] = $"Apiario '{creado.Nombre}' creado (Id {creado.Id}).";

                // Post-Redirect-Get para evitar reenvío de formulario al refrescar
                return RedirectToPage("/Vistas/Dashboard");
                // o: return RedirectToPage();   // si el handler está en la misma página
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await OnGetAsync();
                TempData["OpenApiarioModal"] = true; // reabre el modal con el mensaje de error
                return Page();                       // <<-- ¡no olvides el return!
            }
        }

    }
}
