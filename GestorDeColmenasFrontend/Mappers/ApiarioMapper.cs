using GestorDeColmenasFrontend.Dtos.Apiario;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Mappers
{
    public static class ApiarioMapper
    {
        public static ApiarioListItemDto ToListItemDto(ApiarioModel apiario)
        {
            return new ApiarioListItemDto
            {
                Id = apiario.Id,
                Nombre = apiario.Nombre ?? string.Empty,
                UbicacionDeReferencia = apiario.UbicacionDeReferencia ?? string.Empty,
                CantidadColmenas = apiario.Colmenas?.Count ?? 0,
                HayColmenaEnPeligro = apiario.HayColmenaEnPeligro
            };
        }

        public static List<ApiarioListItemDto> ToListItemDtos(IEnumerable<ApiarioModel> apiarios)
        {
            return apiarios.Select(ToListItemDto).ToList();
        }
    }
}