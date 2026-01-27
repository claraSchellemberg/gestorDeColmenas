using GestorDeColmenasFrontend.Dtos.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages
{
    public class PerfilUsuarioModel : PageModel
    {
        [BindProperty]
        public PerfilUsuarioDto Usuario { get; set; } = new();

        [BindProperty]
        public string? NuevaContrasena { get; set; }

        [BindProperty]
        public string? ConfirmarContrasena { get; set; }
        public void OnGet()
        {
            // Cargar datos del usuario (datos ficticios por ahora)
            Usuario = new PerfilUsuarioDto
            {
                Nombre = "Juan",
                //Apellido = "Pérez",
                Email = "juan.perez@example.com",
                NumeroTelefono = "+54 9 11 1234-5678",
                NumeroApicultor = "AP-12345",
                MedioDeComunicacionDePreferencia = Modelos.CanalPreferidoNotificacion.EMAIL,
                FotoPerfil = "https://i.pravatar.cc/150?img=67"
            };
        }
        public IActionResult OnPostGuardarPerfil()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Validar contraseñas si se ingresaron
            if (!string.IsNullOrEmpty(NuevaContrasena))
            {
                if (NuevaContrasena != ConfirmarContrasena)
                {
                    ModelState.AddModelError("ConfirmarContrasena", "Las contraseñas no coinciden");
                    return Page();
                }
            }

            // TODO: Guardar cambios en el backend
            TempData["ToastSuccess"] = "Perfil actualizado correctamente";

            return RedirectToPage();
        }
    }
}