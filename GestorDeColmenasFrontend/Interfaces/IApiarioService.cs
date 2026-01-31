using GestorDeColmenasFrontend.Dtos;
using GestorDeColmenasFrontend.Dtos.Apiario;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Interfaces
{
    public interface IApiariosService
    {
        Task<List<ApiarioModel>> GetApiarios(int usuarioId);
        Task<ApiarioModel> GetApiarioPorNombreYUsuario(string nombre, int usuarioId);
        Task<DashboardMetricas?> GetMetricasAsync();
        Task<UsuarioSimpleDto?> GetUsuarioAsync();
        Task<ApiarioModel> RegistrarApiarioAsync(ApiarioCreateDto dto);
        Task<ServiceResult> EliminarApiarioAsync(int id);
    }
}
