using GestorDeColmenasFrontend.Dtos.Apiario;
using GestorDeColmenasFrontend.Dtos.Colmena;
using GestorDeColmenasFrontend.Dtos.Mediciones;
using GestorDeColmenasFrontend.Dtos.Registros;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Modelos;

namespace GestorDeColmenasFrontend.Dev
{
    /// <summary>
    /// TEMPORAL: Datos ficticios para desarrollo.
    /// Eliminar esta clase cuando el backend esté conectado.
    /// </summary>
    public static class DatosFicticios
    {
        // ID de usuario ficticio para desarrollo
        public const int UsuarioIdFicticio = 1;

        public static UsuarioSimpleDto GetUsuario() => new()
        {
            Id = UsuarioIdFicticio,
            Nombre = "Juan Pérez",
            Email = "juan.perez@example.com",
            FotoPerfil = "https://i.pravatar.cc/150?img=67"
        };

        public static List<ColmenaListItemDto> GetColmenas() => new()
        {
            new()
            {
                Id = 1, Nombre = "C-045", ApiarioNombre = "El Robledal",
                MedicionesCuadros = new List<MedicionCuadroDto>
                {
                    new() { CuadroNombre = "Cuadro 1", TempInterna1 = 34.5f, TempInterna2 = 35.1f, TempInterna3 = 34.8f },
                    new() { CuadroNombre = "Cuadro 2", TempInterna1 = 34.2f, TempInterna2 = 34.9f, TempInterna3 = 34.6f }
                },
                TempExterna = 28.2f, Peso = 45.2f,
                Estado = CondicionColmena.OPTIMO,
                FechaUltimaMedicion = DateTime.Now.AddHours(-1)
            },
            new()
            {
                Id = 2, Nombre = "C-046", ApiarioNombre = "El Robledal",
                MedicionesCuadros = new List<MedicionCuadroDto>
                {
                    new() { CuadroNombre = "Cuadro 1", TempInterna1 = 35.5f, TempInterna2 = 36.2f, TempInterna3 = 35.8f }
                },
                TempExterna = 30.1f, Peso = 44.1f,
                Estado = CondicionColmena.NECESITA_REVISION,
                FechaUltimaMedicion = DateTime.Now.AddHours(-2)
            },
            new()
            {
                Id = 3, Nombre = "C-047", ApiarioNombre = "El Robledal",
                MedicionesCuadros = new List<MedicionCuadroDto>
                {
                    new() { CuadroNombre = "Cuadro 1", TempInterna1 = 35.8f, TempInterna2 = 36.5f, TempInterna3 = 36.1f },
                    new() { CuadroNombre = "Cuadro 2", TempInterna1 = 36.0f, TempInterna2 = 36.8f, TempInterna3 = 36.3f }
                },
                TempExterna = 31.0f, Peso = 43.9f,
                Estado = CondicionColmena.EN_PELIGRO,
                FechaUltimaMedicion = DateTime.Now.AddMinutes(-30)
            },
            new()
            {
                Id = 4, Nombre = "C-048", ApiarioNombre = "Los Girasoles",
                MedicionesCuadros = new List<MedicionCuadroDto>
                {
                    new() { CuadroNombre = "Cuadro 1", TempInterna1 = 34.0f, TempInterna2 = 34.7f, TempInterna3 = 34.3f }
                },
                TempExterna = 27.1f, Peso = 52.3f,
                Estado = CondicionColmena.OPTIMO,
                FechaUltimaMedicion = DateTime.Now.AddHours(-1)
            },
            new()
            {
                Id = 5, Nombre = "C-049", ApiarioNombre = "Los Girasoles",
                MedicionesCuadros = new List<MedicionCuadroDto>
                {
                    new() { CuadroNombre = "Cuadro 1", TempInterna1 = 33.9f, TempInterna2 = 34.6f, TempInterna3 = 34.2f }
                },
                TempExterna = 26.8f, Peso = 48.1f,
                Estado = CondicionColmena.OPTIMO,
                FechaUltimaMedicion = DateTime.Now.AddHours(-3)
            },
            new()
            {
                Id = 6, Nombre = "C-050", ApiarioNombre = "Los Girasoles",
                MedicionesCuadros = new List<MedicionCuadroDto>(),
                TempExterna = 0, Peso = 0,
                Estado = null,
                FechaUltimaMedicion = null
            },
        };

        public static ColmenaDetalleDto GetColmenaDetalle(int id) => new()
        {
            Id = id,
            Nombre = $"C-{id:D3}",
            ApiarioNombre = "El Robledal",
            Descripcion = "Colmena fuerte, 2 alzas. Reina marcada verde (2024).",
            FechaInstalaciones = new DateTime(2024, 3, 10),
            Estado = CondicionColmena.OPTIMO,
            CantidadCuadros = 3,
            CantidadRegistros = 1234,
            TempInterna1 = 34.5f,
            TempInterna2 = 34.5f,
            TempInterna3 = 34.5f,
            TempExterna = 28.2f,
            Peso = 45.2f
        };

        public static List<RegistroGetDto> GetHistorialMediciones()
        {
            var registros = new List<RegistroGetDto>();
            var baseDate = DateTime.Now;
            var random = new Random(42); // Seed fijo para consistencia

            var estados = new[]
            {
                (CondicionColmena.OPTIMO, "Saludable"),
                (CondicionColmena.NECESITA_REVISION, "Alerta: Cría enfriándose"),
                (CondicionColmena.EN_PELIGRO, "Peligro: Sobrecalentamiento")
            };

            for (int i = 0; i < 10; i++)
            {
                var estadoIndex = i < 5 ? 0 : (i < 8 ? 1 : 2);
                var (estado, mensaje) = estados[estadoIndex];

                registros.Add(new RegistroGetDto
                {
                    Id = i + 1,
                    FechaMedicion = baseDate.AddHours(-i),
                    TempInterna1 = 33.5f + (float)(random.NextDouble() * 2),
                    TempInterna2 = 34.0f + (float)(random.NextDouble() * 2),
                    TempInterna3 = 33.8f + (float)(random.NextDouble() * 2),
                    TempExterna = 18.0f + (float)(random.NextDouble() * 5),
                    Peso = 41.0f + (float)(random.NextDouble() * 2),
                    Estado = estado,
                    MensajeEstado = mensaje
                });
            }

            return registros;
        }

        public static DashboardMetricas GetMetricas() => new()
        {
            Apiarios = 3,
            Colmenas = 24,
            BuenEstado = 21,
            Alertas = 3
        };
    }
}