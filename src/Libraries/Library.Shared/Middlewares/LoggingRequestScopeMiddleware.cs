using System.Threading.Tasks;
using Library.Shared.Constants;
using Microsoft.AspNetCore.Http;
using NLog;

namespace Library.Shared.Middlewares
{
    public class LoggingRequestScopeMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingRequestScopeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (MappedDiagnosticsLogicalContext.SetScoped(LoggingConstants.Scope,
                       LoggingConstants.GetScopeValue(context.TraceIdentifier, context.Request.Method, context.Request.Path)))
                await _next(context);
        }
    }
}