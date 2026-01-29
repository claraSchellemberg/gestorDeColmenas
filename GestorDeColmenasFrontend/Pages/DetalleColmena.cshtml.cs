using GestorDeColmenasFrontend.Dev;
using GestorDeColmenasFrontend.Dtos.Colmena;
using GestorDeColmenasFrontend.Dtos.Registros;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Helpers;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GestorDeColmenasFrontend.Pages 
{
    public class DetalleColmenaModel : PageModel
    {
        private readonly IColmenaService _colmenaService;
        private readonly ILogger<DetalleColmenaModel> _logger;
        //
        private readonly IUsuarioService _usuarioService;

        public DetalleColmenaModel(IColmenaService colmenaService, ILogger<DetalleColmenaModel> logger, IUsuarioService usuarioService)
        {
            _colmenaService = colmenaService;
            _logger = logger;
            _usuarioService = usuarioService;
        }

        public ColmenaDetalleDto? Colmena { get; set; }
        public List<RegistroGetDto> HistorialMediciones { get; set; } = new();
        public UsuarioSimpleDto? Usuario { get; set; }
        public List<string> ErroresCarga { get; set; } = new();

        public int PaginaActual { get; set; } = 1;
        public int TotalRegistros { get; set; }
        public int RegistrosPorPagina { get; set; } = 10;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            //Usuario = DatosFicticios.GetUsuario();
            int usuarioId = SessionHelper.GetUsuarioIdOrDefault(HttpContext.Session);
            Usuario = await _usuarioService.GetUsuarioActualAsync(usuarioId)
                   ?? DatosFicticios.GetUsuario(); // fallback si falla

            try
            {
                Colmena = await _colmenaService.GetColmenaDetalleAsync(id);
                
                if (Colmena is null)
                {
                    TempData["ToastError"] = "La colmena solicitada no existe.";
                    return RedirectToPage("/ListadoColmenas");
                }

                TotalRegistros = Colmena.CantidadRegistros;
                HistorialMediciones = await _colmenaService.GetHistorialMedicionesAsync(id, PaginaActual, RegistrosPorPagina);
                
                // Agregamos log de debugeo
                _logger.LogInformation("HistorialMediciones Count: {Count}", HistorialMediciones.Count);
                foreach (var registro in HistorialMediciones)
                {
                    _logger.LogInformation("Registro: Id={Id}, Tipo={Tipo}, Fecha={Fecha}, TempInt1={Temp1}, Peso={Peso}",
                        registro.Id,
                        registro.TipoRegistro,
                        registro.FechaMedicion,
                        registro.TempInterna1,
                        registro.Peso);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar detalle de colmena {idColmena}", id);
                ErroresCarga.Add($"No se pudo cargar la colmena: {ex.Message}");
            }
            return Page();
        }
    }
}