using HoaM.Application.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HoaM.Extensions.MediatR
{
    public static class ConfigureServices
    {
        public static IServiceCollection UseMediatR(this IServiceCollection services)
        {
            services.AddCommandHandlersFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

            services.AddScoped<ICommandService, MediatrCommandService>();

            return services;
        }

        private static void AddCommandHandlersFromAssemblies(this IServiceCollection services, params Assembly[] assemblies)
        {
            var handlerTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => Array.Exists(t.GetInterfaces(), IsCommandHandlerInterface));

            foreach (var handlerType in handlerTypes)
            {
                var handlerInterfaces = handlerType.GetInterfaces().Where(IsCommandHandlerInterface).ToList();

                foreach (var handlerInterface in handlerInterfaces)
                {
                    services.AddTransient(handlerInterface, handlerType);

                    // Register MediatrCommandHandlerDecorator
                    var commandType = handlerInterface.GetGenericArguments()[0];
                    var responseType = handlerInterface.GetGenericArguments().Length == 2 ? handlerInterface.GetGenericArguments()[1] : null;

                    if (responseType != null)
                    {
                        var mediatrCommandHandlerType = typeof(MediatrCommandHandler<,>).MakeGenericType(commandType, responseType);

                        services.AddTransient(mediatrCommandHandlerType);
                    }
                    else
                    {
                        var mediatrCommandHandlerType = typeof(MediatrCommandHandler<>).MakeGenericType(commandType);

                        services.AddTransient(mediatrCommandHandlerType);
                    }
                }
            }
        }

        private static bool IsCommandHandlerInterface(Type interfaceType)
        {
            return interfaceType.IsGenericType &&
                   (interfaceType.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                    interfaceType.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));
        }
    }
}
