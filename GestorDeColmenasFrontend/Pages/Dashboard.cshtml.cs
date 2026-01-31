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
        private readonly IColmenaService _colmenasService;
        //agrego el servicio de usuario
        private readonly IUsuarioService _usuarioService;
        public DashboardModel(IApiariosService apiariosService, IColmenaService colmenaService, IUsuarioService usuarioService)
        {
            _apiariosService = apiariosService;
            _colmenasService = colmenaService;
            _usuarioService = usuarioService;
        }

        [ViewData]
        public string? ErrorMessage { get; set; }
        public DashboardViewModel ViewModel { get; set; } = new();
        public List<ApiarioModel> Apiarios { get; set; } = new();

        [BindProperty]
        public ApiarioCreateDto NuevoApiarioDto { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                //cargamos el usuario
                int usuarioId = SessionHelper.GetUsuarioIdOrDefault(HttpContext.Session); 
                if (usuarioId != 0)
                {
                    //cargamos los apiarios en el mapa
                    Apiarios = await _apiariosService.GetApiarios(usuarioId);

                    ViewModel = new DashboardViewModel
                    {
                        Metricas = await GetMetricasAsync(),
                        Usuario = await _usuarioService.GetUsuarioActualAsync(usuarioId)
                           ?? new UsuarioSimpleDto() // fallback
                    };
                    return Page();
                }
                else
                {
                    return RedirectToPage("/LoginUsuario");
                }
                
            }
            catch(Exception ex)
            {
                // Expose the error to the Razor page and also keep a TempData fallback
                ErrorMessage = $"Error cargando el dashboard: {ex.Message}";
                TempData["ErrorMessage"] = ErrorMessage;

                // Ensure ViewModel is set so the page can render safely
                ViewModel = new DashboardViewModel
                {
                    Metricas = new DashboardMetricas
                    {
                        Apiarios = Apiarios?.Count ?? 0,
                        Colmenas = 0,
                        BuenEstado = 0,
                        Alertas = 0
                    },
                    Usuario = new UsuarioSimpleDto()
                };

                // Do not return an object from an async Task method
                return Page();
            }
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

        public async Task<DashboardMetricas> GetMetricasAsync()
        {
            try
            {
                var todasLasColmenas = new List<ColmenaModel>();
                //obtenemos las colmenas de todos los apiarios
                foreach (var apiario in Apiarios)
                {
                    var colmenasDelApiario = await _colmenasService.GetColmenasPorApiarioAsync(apiario.Id);
                    todasLasColmenas.AddRange(colmenasDelApiario);
                }
                //clasificamos las colmenas por estado
                var colmenasBuenEstado = todasLasColmenas
                    .Where(c => c.Estado == CondicionColmena.OPTIMO)
                    .ToList();
                var colmenasConAlertas = todasLasColmenas
                    .Where(c => c.Estado == CondicionColmena.NECESITA_REVISION
                    || c.Estado == CondicionColmena.EN_PELIGRO)
                    .ToList();
                return new DashboardMetricas
                {
                    Apiarios = Apiarios?.Count ?? 0,
                    Colmenas = todasLasColmenas.Count,
                    BuenEstado = colmenasBuenEstado.Count,
                    Alertas = colmenasConAlertas.Count
                };
            }
            catch (Exception ex)
            {
                // Surface the error to the UI and return safe defaults
                ErrorMessage = $"Error cargando métricas: {ex.Message}";
                TempData["ErrorMessage"] = ErrorMessage;

                return new DashboardMetricas
                {
                    Apiarios = Apiarios?.Count ?? 0,
                    Colmenas = 0,
                    BuenEstado = 0,
                    Alertas = 0
                };
            }
        }
    }
}
