using System.Security.Claims;
using Library.Shared.Constants;
using Library.Shared.Extensions;
using Microsoft.AspNetCore.Http;

namespace Library.Shared.HttpAccessor
{
    public class HttpAccessor : IReadOnlyHttpAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext.IsAuthenticated();

        public long CurrentUserId => int.Parse(_httpContextAccessor.HttpContext
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value ?? "0");

        public string CurrentJwtToken => _httpContextAccessor.HttpContext.Request.Headers[Headers.Authorization];
    }
}