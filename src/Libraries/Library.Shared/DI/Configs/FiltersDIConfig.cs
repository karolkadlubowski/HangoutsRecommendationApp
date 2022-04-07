using Library.Shared.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Shared.DI.Configs
{
    public static class FiltersDIConfig
    {
        public static IMvcBuilder AddValidationFilter(this IServiceCollection services)
            => services
                .AddMvc(options => options.Filters.Add<ValidationFilter>())
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
    }
}