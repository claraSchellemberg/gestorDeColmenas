using GestorDeColmenasFrontend.Helpers;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages
{
    public class DetallesApiarioModel : PageModel
    {
        private readonly IApiariosService _apiariosService;
        public DetallesApiarioModel(IApiariosService apiariosService)
        {
            _apiariosService = apiariosService;
        }
        public ApiarioModel? Apiario { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if(string.IsNullOrEmpty(Id))
            {
                return RedirectToPage("/ListadoApiarios");
            }
            var usuarioId = SessionHelper.GetUsuarioIdOrDefault(HttpContext.Session);
            try
            {
                Apiario = await _apiariosService.GetApiarioPorNombreYUsuario(Id, usuarioId);
            }
            catch(InvalidOperationException)
            {
                return RedirectToPage("/ListadoApiarios");
            }
            return Page();
        }
    }
}
