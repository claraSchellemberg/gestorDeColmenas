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
        private readonly IColmenaService _colmenasService;
        //agrego el servicio de usuario
        private readonly IUsuarioService _usuarioService;
        public DashboardModel(IApiariosService apiariosService, IColmenaService colmenaService, IUsuarioService usuarioService)
        {
            _apiariosService = apiariosService;
            _colmenasService = colmenaService;
            _usuarioService = usuarioService;
        }

        public DashboardViewModel ViewModel { get; set; } = new();
        public List<ApiarioModel> Apiarios { get; set; } = new();

        [BindProperty]
        public ApiarioCreateDto NuevoApiarioDto { get; set; } = new();

        public async Task OnGetAsync()
        {
            //cargamos el usuario
            int usuarioId = SessionHelper.GetUsuarioIdOrDefault(HttpContext.Session);
            //cargamos los apiarios en el mapa
            Apiarios = await _apiariosService.GetApiarios(usuarioId);

            ViewModel = new DashboardViewModel
            {
                Metricas = await GetMetricasAsync(),
                //Usuario = DatosFicticios.GetUsuario()
                Usuario = await _usuarioService.GetUsuarioActualAsync(usuarioId)
                   ?? new UsuarioSimpleDto() // fallback
            };
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
                Apiarios = Apiarios.Count,
                Colmenas = todasLasColmenas.Count,
                BuenEstado = colmenasBuenEstado.Count,
                Alertas = colmenasConAlertas.Count
            };
        }
    }
}
