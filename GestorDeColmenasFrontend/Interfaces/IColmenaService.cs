using GestorDeColmenasFrontend.Dtos;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Interfaces
{
    public interface IColmenaService
    {
        Task<ColmenaModel> RegistrarAsync(int apiarioId, ColmenaCreateDto dto);
    }
}
