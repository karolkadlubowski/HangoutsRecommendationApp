using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Library.Shared.Extensions
{
    public static class HttpExtensions
    {
        public static bool IsAuthenticated(this HttpContext httpContext)
            => httpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null;
    }
}