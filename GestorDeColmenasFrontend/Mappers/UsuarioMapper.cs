using GestorDeColmenasFrontend.Dtos.Usuario;

namespace GestorDeColmenasFrontend.Mappers
{
    public static class UsuarioMapper
    {
        // Crea un PerfilUsuarioDto a partir de UsuarioCreateDto
        public static PerfilUsuarioDto ToPerfilUsuarioDto(UsuarioCreateDto? dto)
        {
            if (dto is null) return new PerfilUsuarioDto();
            return new PerfilUsuarioDto
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                NumeroTelefono = dto.NumeroTelefono,
                NumeroApicultor = dto.NumeroApicultor,
                MedioDeComunicacionDePreferencia = dto.MedioDeComunicacionDePreferencia,
                FotoPerfil = dto.FotoPerfil
            };
        }

        // Rellena un PerfilUsuarioDto existente desde UsuarioCreateDto
        public static void MapToPerfilUsuarioDto(UsuarioCreateDto? dto, PerfilUsuarioDto perfil)
        {
            if (dto is null || perfil is null) return;
            perfil.Nombre = dto.Nombre;
            perfil.Email = dto.Email;
            perfil.NumeroTelefono = dto.NumeroTelefono;
            perfil.NumeroApicultor = dto.NumeroApicultor;
            perfil.MedioDeComunicacionDePreferencia = dto.MedioDeComunicacionDePreferencia;
            perfil.FotoPerfil = dto.FotoPerfil;
        }

        // Crea UsuarioCreateDto a partir de PerfilUsuarioDto
        public static UsuarioCreateDto ToUsuarioCreateDto(PerfilUsuarioDto? perfil)
        {
            if (perfil is null) return new UsuarioCreateDto();
            return new UsuarioCreateDto
            {
                Nombre = perfil.Nombre,
                Email = perfil.Email,
                NumeroTelefono = perfil.NumeroTelefono,
                NumeroApicultor = perfil.NumeroApicultor,
                MedioDeComunicacionDePreferencia = perfil.MedioDeComunicacionDePreferencia,
                FotoPerfil = perfil.FotoPerfil
            };
        }
    }
}
