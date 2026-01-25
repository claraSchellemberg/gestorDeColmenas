using GestorDeColmenasFrontend.Dev;
using GestorDeColmenasFrontend.Dtos.Apiario;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Helpers;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Mappers;
using GestorDeColmenasFrontend.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages
{
    public class ListadoApiariosModel : PageModel
    {
        private readonly IApiariosService _apiariosService;
        //lo que agregue a partir de usuarios
        private readonly IUsuarioService _usuarioService = new UsuarioService();
        public ListadoApiariosModel(IApiariosService apiariosService, IUsuarioService usuarioService)
        {
            _apiariosService = apiariosService;
            _usuarioService = usuarioService;
        }
        public List<ApiarioListItemDto> Apiarios { get; set; } = new();
        public UsuarioSimpleDto? Usuario { get; set; }

        [BindProperty]
        public ApiarioCreateDto NuevoApiario { get; set; } = new();

        public async Task OnGetAsync()
        {
            // TODO: Reemplazar con llamadas a servicios cuando el backend esté listo
            //Usuario = DatosFicticios.GetUsuario();
            int usuarioId = SessionHelper.GetUsuarioIdOrDefault(HttpContext.Session);
            Usuario = await _usuarioService.GetUsuarioActualAsync(usuarioId)
                   ?? DatosFicticios.GetUsuario(); // fallback si falla

            var apiarioModels = await _apiariosService.GetApiarios(usuarioId);
            Apiarios = ApiarioMapper.ToListItemDtos(apiarioModels);
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
                // Set the user ID from session (or default fake ID)
                NuevoApiario.UsuarioId = SessionHelper.GetUsuarioIdOrDefault(HttpContext.Session);
                
                await _apiariosService.RegistrarApiarioAsync(NuevoApiario);
                TempData["ToastSuccess"] = $"Apiario '{NuevoApiario.Nombre}' creado correctamente.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await OnGetAsync();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEliminarApiarioAsync(int id)
        {
            var result = await _apiariosService.EliminarApiarioAsync(id);

            return new JsonResult(new 
            { 
                success = result.Success, 
                message = result.ErrorMessage 
            });
        }
    }
}
