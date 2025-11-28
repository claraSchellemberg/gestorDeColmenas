using GestorDeColmenasFrontend.Dtos;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Interfaces
{
    public interface IApiariosService
    {
        Task<DashboardMetricas> GetMetricasAsync();
        Task<MapaViewModel> GetMapaAsync();
        Task<UsuarioViewModel> GetUsuarioAsync();

        Task<ApiarioModel> RegistrarApiarioAsync(ApiarioCreateDto dto);
    }

}
