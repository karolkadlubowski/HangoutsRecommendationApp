using Library.Shared.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Shared.DI.Configs
{
    public static class PipelineBehavioursDIConfig
    {
        public static IServiceCollection AddPipelineBehaviours(this IServiceCollection services)
            => services
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }
}