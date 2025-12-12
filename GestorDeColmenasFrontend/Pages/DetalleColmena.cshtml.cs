using GestorDeColmenasFrontend.Dev;
using GestorDeColmenasFrontend.Dtos;
using GestorDeColmenasFrontend.Dtos.Colmena;
using GestorDeColmenasFrontend.Dtos.Mediciones;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages 
{
    public class DetalleColmenaModel : PageModel
    {
        public ColmenaDetalleDto? Colmena { get; set; }
        public List<RegistroMedicionDto> HistorialMediciones { get; set; } = new();
        public UsuarioSimpleDto? Usuario { get; set; }

        public int PaginaActual { get; set; } = 1;
        public int TotalRegistros { get; set; }
        public int RegistrosPorPagina { get; set; } = 10;

        public void OnGet(int id)
        {
            // TODO: Reemplazar con llamadas a servicios cuando el backend esté listo
            Usuario = DatosFicticios.GetUsuario();
            Colmena = DatosFicticios.GetColmenaDetalle(id);
            HistorialMediciones = DatosFicticios.GetHistorialMediciones();
            TotalRegistros = Colmena?.CantidadRegistros ?? 0;
        }
    }
}