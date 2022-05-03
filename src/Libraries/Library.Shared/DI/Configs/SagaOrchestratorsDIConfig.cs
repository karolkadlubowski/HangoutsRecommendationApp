using System.Reflection;
using Library.Shared.Events.Transaction.Abstractions;
using Library.Shared.Events.Transaction.Factories;
using Library.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Shared.DI.Configs
{
    public static class SagaOrchestratorsDIConfig
    {
        public static IServiceCollection AddSagaOrchestrators(this IServiceCollection services, Assembly sagaOrchestratorsAssembly)
            => services
                .AddSingleton<ISagaOrchestratorFactory, SagaOrchestratorFactory>()
                .RegisterAllTypes<ISagaOrchestrator>(new[] { sagaOrchestratorsAssembly }, ServiceLifetime.Singleton);
    }
}