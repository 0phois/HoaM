using HoaM.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HoaM.Infrastructure
{
    public sealed class InfrastructureBuilder(IServiceCollection services, IConfiguration configuration) 
    {
        public IServiceCollection ServiceCollection { get; } = services; 
        public IConfiguration Configuration { get; } = configuration;
    }

    public static class ConfigureServices
    {
        public static InfrastructureBuilder AddIdentityServices<TUser>(this IServiceCollection services, IConfiguration configuration) where TUser : IdentityUser, new()
        {
            services.AddAuthorization();
            services.AddIdentityApiEndpoints<TUser>()
                    .AddEntityFrameworkStores<IdentityDbContext<TUser>>();

            services.Configure<IdentityOptions>(configuration.GetSection(nameof(IdentityOptions)));

            return new InfrastructureBuilder(services, configuration);
        }

        public static IServiceCollection ConfigureDbContext<TContext>(this InfrastructureBuilder builder,
                                                                      IOptions<DatabaseOptions>? dbOptions = null,
                                                                      Action<DbContextOptionsBuilder>? ctxBuilder = null) where TContext : DbContext
        {
            var optionsBuilder = ctxBuilder;
            var databaseOptions = dbOptions?.Value ?? new();
            var services = builder.ServiceCollection;
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<TContext>((sp, options) =>
            {
                optionsBuilder?.Invoke(options);

                if (databaseOptions.ProviderType != ProviderType.InMemory)
                    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

                options = databaseOptions.ProviderType switch
                {
                    ProviderType.InMemory => options,
                    ProviderType.PostgreSQL => options.UseNpgsql(connectionString, contextBuilder => contextBuilder.MigrationsAssembly(databaseOptions.MigrationsAssembly).UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)),
                    ProviderType.SQLServer => options.UseSqlServer(connectionString, contextBuilder => contextBuilder.MigrationsAssembly(databaseOptions.MigrationsAssembly).UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)),
                    ProviderType.SQLite => options.UseSqlite(connectionString, contextBuilder => contextBuilder.MigrationsAssembly(databaseOptions.MigrationsAssembly).UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)),
                    _ => throw new InvalidOperationException("Unsupported database")
                };

            });
            
            services.AddScoped(provider => provider.GetRequiredService<TContext>());

            //TODO - get assemblies with IEntity | Register type configurations
            return services; 
        }
    }
}
