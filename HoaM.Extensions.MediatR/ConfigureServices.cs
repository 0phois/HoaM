using HoaM.Application.Common;
using HoaM.Domain.Common;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace HoaM.Extensions.MediatR
{
    public static class ConfigureServices
    {
        /// <summary>
        /// Configures MediatR and registers related services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
        public static IServiceCollection UseMediatR(this IServiceCollection services)
        {
            services.RegisterCommandHandlerAdapters();

            //TODO - register notification handler adapters

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                      .AddCommandBinders().Lifetime = ServiceLifetime.Scoped;
            });

            services.AddScoped<ICommandService, MediatrCommandService>();
            services.AddScoped<IDomainEventDispatcher, MediatrDomainEventDispatcher>();

            return services;
        }

        private static MediatRServiceConfiguration AddCommandBinders(this MediatRServiceConfiguration configuration)
        {
            configuration.AddOpenBehavior(typeof(RequestPreProcessorBehavior<,>), ServiceLifetime.Scoped);
            configuration.AddOpenRequestPreProcessor(typeof(CommunityBinder<>), ServiceLifetime.Scoped);
            configuration.AddOpenRequestPreProcessor(typeof(CommitteeBinder<>), ServiceLifetime.Scoped);
            configuration.AddOpenRequestPreProcessor(typeof(PropertyBinder<>), ServiceLifetime.Scoped);
            configuration.AddOpenRequestPreProcessor(typeof(AssociationMemberBinder<>), ServiceLifetime.Scoped);

            return configuration;
        }

        private static void RegisterCommandHandlerAdapters(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var handlerTypes = assemblies.SelectMany(a => a.GetTypes())
                                         .Where(t => Array.Exists(t.GetInterfaces(), IsCommandHandlerInterface));

            foreach (var handlerType in handlerTypes)
            {
                var interfaceType = Array.Find(handlerType.GetInterfaces(), IsCommandHandlerInterface);

                if (interfaceType is not null)
                {
                    RegisterMediatrRequestAdapters(services, interfaceType, handlerType);
                }
            }
        }

        private static void RegisterMediatrRequestAdapters(IServiceCollection services, Type interfaceType, Type handlerType)
        {
            services.AddScoped(interfaceType, handlerType);

            var genericArguments = interfaceType.GetGenericArguments();
            var mediatrRequestType = typeof(MediatrRequestAdapter<,>).MakeGenericType(genericArguments);
            var mediatrRequestHandlerType = typeof(MediatrRequestHandlerAdapter<,>).MakeGenericType(genericArguments);
            var mediatrResultType = genericArguments[1].IsGenericType
                ? typeof(IResult<>).MakeGenericType(genericArguments[1].GetGenericArguments()[0])
                : genericArguments[1];

            var requestHandlerType = typeof(IRequestHandler<,>).MakeGenericType(mediatrRequestType, mediatrResultType);

            services.AddScoped(requestHandlerType, mediatrRequestHandlerType);
        }

        private static bool IsCommandHandlerInterface(Type type)
            => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ICommandHandler<,>);
    }
}
