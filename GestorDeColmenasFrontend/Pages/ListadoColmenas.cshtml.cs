using GestorDeColmenasFrontend.Dev;
using GestorDeColmenasFrontend.Dtos.Colmena;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Helpers;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Modelos;
using GestorDeColmenasFrontend.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages
{
    public class ListadoColmenasModel : PageModel
    {
        private readonly IColmenaService _colmenaService;
        private readonly IApiariosService _apiarioService;
        private readonly ILogger<ListadoColmenasModel> _logger;
        //lo que agregue a partir de usuarios
        private readonly IUsuarioService _usuarioService = new UsuarioService();

        public ListadoColmenasModel(
            IColmenaService colmenaService, 
            IApiariosService apiarioService,
            ILogger<ListadoColmenasModel> logger,
            IUsuarioService usuarioService)
        {
            _colmenaService = colmenaService;
            _apiarioService = apiarioService;
            _logger = logger;
            _usuarioService = usuarioService;
        }

        public List<ColmenaListItemDto> Colmenas { get; set; } = new();
        public List<ApiarioModel> Apiarios { get; set; } = new();
        public UsuarioSimpleDto? Usuario { get; set; }

        [BindProperty]
        public ColmenaCreateDto NuevaColmena { get; set; } = new();

        public bool MostrarModal { get; set; }

        //guarda todos los errores que pueden pasar al cargador los datos        
        public List<string> ErroresCarga { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Estado { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? ApiarioId { get; set; }
        
        public string? ApiarioNombreFiltro { get; set; }
        public async Task OnGetAsync()
        {
            await CargarDatosConErrores();
            //filtrar si viene el parametro apiarioId
            if(ApiarioId.HasValue)
            {
                Colmenas = Colmenas.Where(c => c.ApiarioId == ApiarioId.Value).ToList();
            }
            //filtrar si viene el parametro estado
            if (!string.IsNullOrEmpty(Estado))
            {
                Colmenas = Estado switch
                {
                    "OPTIMO" => Colmenas.Where(c => c.Estado == CondicionColmena.OPTIMO).ToList(),
                    "ALERTA" => Colmenas.Where(c => c.Estado != CondicionColmena.OPTIMO).ToList(),
                    _ => Colmenas
                };
            }
        }

        public async Task<IActionResult> OnPostAgregarColmenaAsync()
        {
            if (!ModelState.IsValid)
            {
                MostrarModal = true;
                await CargarDatosConErrores();
                return Page();
            }

            try
            {
                if (!NuevaColmena.ApiarioId.HasValue)
                {
                    ModelState.AddModelError("NuevaColmena.ApiarioId", "Debe seleccionar un apiario.");
                    MostrarModal = true;
                    await CargarDatosConErrores();
                    return Page();
                }

                var colmenaCreada = await _colmenaService.RegistrarAsync(
                    NuevaColmena.ApiarioId.Value, 
                    NuevaColmena
                );

                TempData["ToastSuccess"] = $"Colmena '{colmenaCreada.Nombre}' creada correctamente.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar colmena");
                ModelState.AddModelError(string.Empty, ex.Message);
                MostrarModal = true;
                await CargarDatosConErrores();
                return Page();
            }
        }

        /// <summary>
        /// Loads all data, collecting errors instead of silently ignoring them.
        /// This allows the page to render partially while still informing the user of issues.
        /// </summary>
        private async Task CargarDatosConErrores()
        {
            //Usuario = DatosFicticios.GetUsuario();
            int usuarioId = SessionHelper.GetUsuarioIdOrDefault(HttpContext.Session);
            Usuario = await _usuarioService.GetUsuarioActualAsync(usuarioId)
                   ?? DatosFicticios.GetUsuario(); // fallback si falla

            try
            {
                Colmenas = await _colmenaService.GetColmenasAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar colmenas");
                ErroresCarga.Add($"No se pudieron cargar las colmenas: {ex.Message}");
                Colmenas = new List<ColmenaListItemDto>();
            }

            try
            {
                Apiarios = await _apiarioService.GetApiarios(usuarioId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar apiarios");
                ErroresCarga.Add($"No se pudieron cargar los apiarios: {ex.Message}");
                Apiarios = new List<ApiarioModel>();
            }
        }
    }    
}
