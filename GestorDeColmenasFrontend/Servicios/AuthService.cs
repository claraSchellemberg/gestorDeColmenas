using GestorDeColmenasFrontend.Helpers;
using GestorDeColmenasFrontend.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace GestorDeColmenasFrontend.Servicios
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IHttpContextAccessor httpContextAccessor, ILogger<AuthService> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task LogoutAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                httpContext.Session.Clear();
            }
            else
            {
                _logger.LogWarning("HttpContext is null. Cannot clear session on logout.");
            }
            try
            {
                var cookieKeys = httpContext?.Request.Cookies.Keys.ToList() ?? new List<string>();
                foreach (var cookieKey in cookieKeys)
                {
                    if (cookieKey.StartsWith("Auth", StringComparison.OrdinalIgnoreCase)
                        || cookieKey.StartsWith(".AspNetCore", StringComparison.OrdinalIgnoreCase)
                        || cookieKey.Equals("UsuarioCorreo", StringComparison.OrdinalIgnoreCase)
                        || cookieKey.Equals("UsuarioNombre", StringComparison.OrdinalIgnoreCase))
                    {
                        httpContext.Response.Cookies.Delete(cookieKey);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting authentication cookies.");
            }
        }
    }
}
