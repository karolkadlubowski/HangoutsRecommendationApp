using Library.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Library.Shared.DI.Configs
{
    public static class MiddlewaresDIConfig
    {
        public static IApplicationBuilder UseLoggingRequestScope(this IApplicationBuilder app)
            => app.UseMiddleware<LoggingRequestScopeMiddleware>();
    }
}