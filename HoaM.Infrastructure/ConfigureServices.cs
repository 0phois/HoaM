using HoaM.Domain;
using HoaM.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HoaM.Infrastructure
{
    public sealed class InfrastructureBuilder<TDbContext>(IServiceCollection services, IConfiguration configuration) where TDbContext : DbContext
    {
        public IServiceCollection ServiceCollection { get; } = services; 
        public IConfiguration Configuration { get; } = configuration;
    }

    public static class ConfigureServices
    {
        public static InfrastructureBuilder<TDbContext> AddIdentityServices<TUser, TRole, TDbContext>(this IServiceCollection services,
                                                                                   IConfiguration configuration,
                                                                                   Action<IdentityBuilder>? identityBuilder = null)
            where TUser : class, new()
            where TRole : class
            where TDbContext : DbContext
        {
            services.AddAuthorization();

            var defaultIdentityBuilder = services.AddIdentityApiEndpoints<TUser>().AddRoles<TRole>()
                                                 .AddEntityFrameworkStores<TDbContext>();
            
            identityBuilder?.Invoke(defaultIdentityBuilder);

            services.Configure<IdentityOptions>(configuration.GetSection(nameof(IdentityOptions)));

            return new InfrastructureBuilder<TDbContext>(services, configuration);
        }

        public static InfrastructureBuilder<TContext> ConfigureDbContext<TContext>(this InfrastructureBuilder<TContext> builder, Action<DbContextOptionsBuilder>? ctxBuilder = null) where TContext : DbContext
        {
            var optionsBuilder = ctxBuilder;
            var services = builder.ServiceCollection;
            var databaseOptions = builder.Configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>() ?? new();

            var connectionString = string.IsNullOrEmpty(databaseOptions.ConnectionString) 
                ? builder.Configuration.GetConnectionString("DefaultConnection")
                : databaseOptions.ConnectionString;

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, SoftDeleteEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<TContext>((provider, options) =>
            {
                options.ReplaceService<IValueConverterSelector, TypelyValueConverterSelector>();

                optionsBuilder?.Invoke(options);

                if (databaseOptions.ProviderType != ProviderType.InMemory)
                {
                    options.AddInterceptors(provider.GetServices<ISaveChangesInterceptor>());
                }

                options = databaseOptions.ProviderType switch
                {
                    ProviderType.InMemory => options,
                    ProviderType.PostgreSQL => options.UseNpgsql(connectionString, contextBuilder => contextBuilder.MigrationsAssembly(databaseOptions.MigrationsAssembly).UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)),
                    ProviderType.SQLServer => options.UseSqlServer(connectionString, contextBuilder => contextBuilder.MigrationsAssembly(databaseOptions.MigrationsAssembly).UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)),
                    ProviderType.SQLite => options.UseSqlite(connectionString, contextBuilder => contextBuilder.MigrationsAssembly(databaseOptions.MigrationsAssembly).UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)),
                    _ => throw new InvalidOperationException("Unsupported database provider")
                };

            });

            services.AddScoped<DbContext, TContext>();

            return builder; 
        }

        public static IServiceCollection WithDefaultRepositories<TContext>(this InfrastructureBuilder<TContext> builder) where TContext : DbContext
        {
            var services = builder.ServiceCollection;

            services.ConfigureRepositories();

            //services.AddScoped(typeof(IEventRepository<>), typeof(EventRepository<>));
            //services.AddScoped(typeof(IBaseRepository<,>), typeof(GenericRepository<,>));

            return services;
        }

        private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            var repositoryTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.IsClass &&
                               !type.IsAbstract &&
                               type.BaseType != null &&
                               type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(GenericRepository<,>));

            foreach (var repositoryType in repositoryTypes)
            {
                var entityType = repositoryType.BaseType?.GetGenericArguments()[0];
                var entityIdType = repositoryType.BaseType?.GetGenericArguments()[1];

                if (entityType!.FullName is null) continue;

                var genericRepositoryType = typeof(GenericRepository<,>).MakeGenericType(entityType!, entityIdType!);
                services.AddScoped(genericRepositoryType, repositoryType);

                var interfaceType = Array.Find(repositoryType.GetInterfaces(), inter => inter.Name == $"I{entityType!.Name}Repository");

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, repositoryType);
                }
            }

            return services;
        }

    }
}
