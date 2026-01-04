using GestorDeColmenasFrontend.Dev;
using GestorDeColmenasFrontend.Dtos.Colmena;
using GestorDeColmenasFrontend.Dtos.Mediciones;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages
{
    public class ListadoColmenasModel : PageModel
    {
        private readonly IColmenaService _colmenaService;
        private readonly IApiariosService _apiarioService;

        public ListadoColmenasModel(IColmenaService colmenaService, IApiariosService apiarioService)
        {
            _colmenaService = colmenaService;
            _apiarioService = apiarioService;
        }

        public List<ColmenaListItemDto> Colmenas { get; set; } = new();
        public List<ApiarioModel> Apiarios { get; set; } = new();
        public UsuarioSimpleDto? Usuario { get; set; }

        [BindProperty]
        public ColmenaCreateDto NuevaColmena { get; set; } = new();

        // Flag to keep modal open when there are validation errors
        public bool MostrarModal { get; set; }

        public async Task OnGetAsync()
        {
            // TODO: Reemplazar con llamadas a servicios cuando el backend
            // esté listo
            Usuario = DatosFicticios.GetUsuario();
            Colmenas = await _colmenaService.GetColmenasAsync();
            Apiarios = await _apiarioService.GetApiarios();
        }

        public async Task<IActionResult> OnPostAgregarColmenaAsync()
        {
            if (!ModelState.IsValid)
            {
                MostrarModal = true;
                await OnGetAsync();
                return Page();
            }

            try
            {
                // 1. Validate that an Apiario was selected
                if (!NuevaColmena.ApiarioId.HasValue)
                {
                    ModelState.AddModelError("NuevaColmena.ApiarioId", "Debe seleccionar un apiario.");
                    MostrarModal = true;
                    await OnGetAsync();
                    return Page();
                }

                // 2. Call the service to register the new Colmena
                var colmenaCreada = await _colmenaService.RegistrarAsync(
                    NuevaColmena.ApiarioId.Value, 
                    NuevaColmena
                );

                // 3. Show success message
                TempData["ToastSuccess"] = $"Colmena '{colmenaCreada.Nombre}' creada correctamente.";
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                // 4. Handle errors and show them to the user
                ModelState.AddModelError(string.Empty, ex.Message);
                MostrarModal = true;
                await OnGetAsync();
                return Page();
            }
        }
    }    
}
