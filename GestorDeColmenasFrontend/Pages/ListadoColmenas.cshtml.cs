using GestorDeColmenasFrontend.Dev;
using GestorDeColmenasFrontend.Dtos.Colmena;
using GestorDeColmenasFrontend.Dtos.Mediciones;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages
{
    public class ListadoColmenasModel : PageModel
    {
        public List<ColmenaListItemDto> Colmenas { get; set; } = new();
        public UsuarioSimpleDto? Usuario { get; set; }

        [BindProperty]
        public ColmenaCreateDto NuevaColmena { get; set; } = new();

        public void OnGet()
        {
            // TODO: Reemplazar con llamadas a servicios cuando el backend
            // esté listo
            Usuario = DatosFicticios.GetUsuario();
            Colmenas = DatosFicticios.GetColmenas();
        }
        public IActionResult OnPostAgregarColmena()
        {
            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }

            // TODO: Guardar colmena en el backend
            TempData["ToastSuccess"] = $"Colmena '{NuevaColmena.Nombre}' creada correctamente.";
            return RedirectToPage();
        }
    }    
}
