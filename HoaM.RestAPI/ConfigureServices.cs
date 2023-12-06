using Microsoft.Extensions.DependencyInjection;
using Typely.AspNetCore.Swashbuckle;

namespace HoaM.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddEndpointServices(this IServiceCollection services)
        {

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options => options.UseTypelySchemaFilter());

            return services;
        }
    }
}
