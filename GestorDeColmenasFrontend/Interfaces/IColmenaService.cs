using GestorDeColmenasFrontend.Dtos.Colmena;
using GestorDeColmenasFrontend.Dtos.Registros;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Interfaces
{
    public interface IColmenaService
    {
        Task<ColmenaModel> RegistrarAsync(int apiarioId, ColmenaCreateDto dto);
        Task<List<ColmenaListItemDto>> GetColmenasAsync();
        Task<ColmenaDetalleDto?> GetColmenaDetalleAsync(int id);
        Task<List<RegistroGetDto>> GetHistorialMedicionesAsync(int idColmena, int pagina = 1, int registrosPorPagina = 10);
        Task<List<ColmenaModel>> GetColmenasPorApiarioAsync(int apiarioId);
    }
}
