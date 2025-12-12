using GestorDeColmenasFrontend.Dtos.Apiario;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Interfaces
{
    public interface IApiariosService
    {
        Task<DashboardMetricas> GetMetricasAsync();
        Task<UsuarioSimpleDto> GetUsuarioAsync();
        Task<ApiarioModel> RegistrarApiarioAsync(ApiarioCreateDto dto);
    }

}
