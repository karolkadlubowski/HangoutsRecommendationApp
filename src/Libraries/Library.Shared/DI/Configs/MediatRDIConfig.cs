using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Shared.DI.Configs
{
    public static class MediatRDIConfig
    {
        public static IServiceCollection AddMediatRWithValidators(this IServiceCollection services, Assembly assembly)
            => services
                .AddMediatR(assembly)
                .AddValidatorsFromAssembly(assembly);
    }
}