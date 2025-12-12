using GestorDeColmenasFrontend.Dev;
using GestorDeColmenasFrontend.Dtos.Apiario;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages
{
    public class ListadoApiariosModel : PageModel
    {
        private readonly IApiariosService _apiariosService;

        public ListadoApiariosModel(IApiariosService apiariosService)
        {
            _apiariosService = apiariosService;
        }

        public List<ApiarioListItemDto> Apiarios { get; set; } = new();
        public UsuarioSimpleDto? Usuario { get; set; }

        [BindProperty]
        public ApiarioCreateDto NuevoApiario { get; set; } = new();

        public async Task OnGetAsync()
        {
            // TODO: Reemplazar con llamadas a servicios cuando el backend esté listo
            Usuario = DatosFicticios.GetUsuario();
            Apiarios = DatosFicticios.GetApiarios();
        }

        public async Task<IActionResult> OnPostAgregarApiarioAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            try
            {
                var creado = await _apiariosService.RegistrarApiarioAsync(NuevoApiario);
                TempData["ToastSuccess"] = $"Apiario '{creado.Nombre}' creado correctamente.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await OnGetAsync();
                return Page();
            }
        }
    }
}
