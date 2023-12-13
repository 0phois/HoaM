using Microsoft.Extensions.DependencyInjection;

namespace HoaM.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection UseDefaultTimeProvider(this IServiceCollection services)
        {
            services.AddSingleton(TimeProvider.System);

            return services;
        }
    }
}
