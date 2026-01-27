using GestorDeColmenasFrontend.Dtos;
using GestorDeColmenasFrontend.Dtos.Usuario;
using GestorDeColmenasFrontend.Interfaces;
using GestorDeColmenasFrontend.Modelos;
using System.Text.Json;

namespace GestorDeColmenasFrontend.Servicios
{
    /// <summary>
    /// Implementación del servicio de usuario.
    /// Maneja la comunicación con el backend para autenticación y gestión de perfil.
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(HttpClient http, ILogger<UsuarioService> logger)
        {
            _http = http;
            _logger = logger;
        }

        /// <summary>
        /// Autentica un usuario con sus credenciales
        /// </summary>
        public async Task<UsuarioSimpleDto?> AutenticarAsync(LoginUsuarioDto credenciales)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("login/login", credenciales);
                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadFromJsonAsync<TokenResponseDto>();
                    if (resultado?.Token != null)
                    {
                        ObtenerUsuarioCompletoDto? usuarioDelToken = null;
                        try
                        {
                            usuarioDelToken = JsonSerializer.Deserialize<ObtenerUsuarioCompletoDto>(resultado.Token);
                        }
                        catch (JsonException ex)
                        {
                            _logger.LogError(ex, "Error al deserializar el token para el usuario {Email}", credenciales.Email);
                            return null;
                        }
                        if (usuarioDelToken == null)
                        {
                            _logger.LogWarning("No se pudo extraer los datos del usuario del token para {Email}", credenciales.Email);
                            return null;
                        }
                        _logger.LogInformation("Usuario {Email} autenticado exitosamente. ID: {UserId}", credenciales.Email, usuarioDelToken.Id);
                        return new UsuarioSimpleDto
                        {
                            Id = usuarioDelToken.Id,
                            Nombre = usuarioDelToken.Nombre ?? credenciales.Email,
                            Email = usuarioDelToken.Email ?? credenciales.Email,
                            FotoPerfil = null
                        };
                    }
                    _logger.LogWarning("Token nulo en la respuesta de autenticación para el usuario {Email}", credenciales.Email);
                    return null;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Error de autenticación: {StatusCode} - {ErrorContent}", response.StatusCode, errorContent);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al autenticar usuario");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al autenticar usuario");
                return null;
            }
        }

        /// <summary>
        /// Obtiene el perfil completo del usuario
        /// </summary>
        public async Task<UsuarioCreateDto?> GetPerfilAsync(int usuarioId)
        {
            try
            {
                var response = await _http.GetAsync($"Usuarios/{usuarioId}/perfil");

                if (response.IsSuccessStatusCode)
                {
                    var perfil = await response.Content.ReadFromJsonAsync<UsuarioCreateDto>();
                    return perfil;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Error al obtener perfil: {StatusCode} - {ErrorContent}", response.StatusCode, errorContent);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al obtener perfil del usuario {UsuarioId}", usuarioId);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener perfil del usuario {UsuarioId}", usuarioId);
                return null;
            }
        }

        /// <summary>
        /// Actualiza el perfil del usuario
        /// </summary>
        public async Task<bool> ActualizarPerfilAsync(int usuarioId, UsuarioCreateDto perfil)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"Usuarios/{usuarioId}/perfil", perfil);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Perfil del usuario {UsuarioId} actualizado exitosamente", usuarioId);
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Error al actualizar perfil del usuario {UsuarioId}: {StatusCode} - {ErrorContent}", usuarioId, response.StatusCode, errorContent);
                    return false;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al actualizar perfil del usuario {UsuarioId}", usuarioId);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar perfil del usuario {UsuarioId}", usuarioId);
                return false;
            }
        }

        /// <summary>
        /// Obtiene los datos mínimos del usuario actual (para header/sidebar)
        /// </summary>
        public async Task<UsuarioSimpleDto?> GetUsuarioActualAsync(int usuarioId)
        {
            try
            {
                var response = await _http.GetAsync($"Usuarios/{usuarioId}");

                if (response.IsSuccessStatusCode)
                {
                    var usuario = await response.Content.ReadFromJsonAsync<UsuarioSimpleDto>();
                    return usuario;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Error al obtener usuario actual: {StatusCode} - {ErrorContent}", response.StatusCode, errorContent);
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al obtener usuario actual {UsuarioId}", usuarioId);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener usuario actual {UsuarioId}", usuarioId);
                return null;
            }
        }

        /// <summary>
        /// Registra un nuevo usuario en el backend
        /// </summary>
        public async Task<RegistroUsuarioModel> RegistrarUsuarioAsync(UsuarioCreateDto dto)
        {
            try
            {
                var resp = await _http.PostAsJsonAsync("Usuarios", dto);

                if (!resp.IsSuccessStatusCode)
                {
                    var body = await resp.Content.ReadAsStringAsync();
                    _logger.LogWarning("Error creando usuario: {StatusCode} - {Body}", resp.StatusCode, body);
                    throw new InvalidOperationException($"Error creando usuario: {(int)resp.StatusCode} {body}");
                }

                var usuarioCreado = await resp.Content.ReadFromJsonAsync<RegistroUsuarioModel>();
                if (usuarioCreado is null)
                {
                    _logger.LogWarning("El backend devolvió contenido vacío al crear usuario.");
                    throw new InvalidOperationException("El backend no devolvió el usuario creado.");
                }

                _logger.LogInformation("Usuario creado exitosamente. Email: {Email} Id: {Id}", usuarioCreado.Email, usuarioCreado.Id);
                return usuarioCreado;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error de conexión al crear usuario {Email}", dto?.Email);
                throw;
            }
        }
    }
}