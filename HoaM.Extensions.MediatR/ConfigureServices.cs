using HoaM.Application.Common;
using HoaM.Domain.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HoaM.Extensions.MediatR
{
    public static class ConfigureServices
    {
        public static IServiceCollection UseMediatR(this IServiceCollection services)
        {
            services.RegisterCommandHandlerDecorators(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

            services.AddScoped<ICommandService, MediatrCommandService>();

            return services;
        }

        private static void RegisterCommandHandlerDecorators(this IServiceCollection services, Assembly[] assemblies)
        {
            var handlerTypes = assemblies.SelectMany(a => a.GetTypes())
                                         .Where(t => Array.Exists(t.GetInterfaces(), IsCommandHandlerInterface));

            foreach (var handlerType in handlerTypes)
            {
                var interfaceType = Array.Find(handlerType.GetInterfaces(), IsCommandHandlerInterface);

                if (interfaceType is not null && GetMediatrHandlerType(interfaceType) is { } mediatrHandlerType)
                {
                    RegisterDecoratorServices(services, interfaceType, handlerType, mediatrHandlerType);
                }
            }
        }

        private static void RegisterDecoratorServices(IServiceCollection services, Type interfaceType, Type handlerType, Type mediatrHandlerType)
        {
            services.AddTransient(interfaceType, handlerType);

            var genericArguments = interfaceType.GetGenericArguments();
            var mediatrCommandType = GetMediatrCommand(genericArguments).MakeGenericType(genericArguments);
            var mediatrHandlerGenericType = mediatrHandlerType.MakeGenericType(genericArguments);

            var requestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(mediatrCommandType, genericArguments.Length == 1 ? typeof(IResult) : typeof(IResult<>).MakeGenericType(genericArguments[1]));

            services.AddTransient(requestHandlerType, mediatrHandlerGenericType);
        }

        private static bool IsCommandHandlerInterface(Type type)
        {
            return type.IsGenericType &&
                   (type.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                    type.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));
        }

        private static Type GetMediatrCommand(Type[] genericArguments)
        {
            return genericArguments.Length switch
            {
                1 => typeof(MediatrCommand<>),
                2 => typeof(MediatrCommand<,>),
                _ => throw new ArgumentOutOfRangeException(nameof(genericArguments), "Generic arguments length is outside of the expected range")
            };
        }

        private static Type GetMediatrHandlerType(Type commandHandlerInterface)
        {
            var genericArguments = commandHandlerInterface.GetGenericArguments();

            return genericArguments.Length switch
            {
                1 => typeof(MediatrCommandHandler<>),
                2 => typeof(MediatrCommandHandler<,>),
                _ => throw new ArgumentOutOfRangeException(nameof(commandHandlerInterface), "Generic arguments length is outside of the expected range")
            };
        }
    }
}
