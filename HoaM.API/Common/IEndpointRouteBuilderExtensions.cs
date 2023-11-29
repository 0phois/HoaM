using System.Reflection;

namespace HoaM.API.Features
{
    public static class IEndpointRouteBuilderExtensions
    {
        public static RouteGroupBuilder MapGroup(this IEndpointRouteBuilder builder, EndpointGroupBase group)
        {
            var groupName = group.GetType().Name;

            return builder.MapGroup($"/api/{groupName}")
                          .WithTags(groupName)
                          .WithOpenApi();
        }

        public static IEndpointRouteBuilder MapFeatureEndpoints(this IEndpointRouteBuilder builder)
        {
            var endpointGroupType = typeof(EndpointGroupBase);

            var assembly = Assembly.GetExecutingAssembly();

            var endpointGroupTypes = assembly.GetExportedTypes().Where(t => t.IsSubclassOf(endpointGroupType));

            foreach (var type in endpointGroupTypes)
            {
                if (Activator.CreateInstance(type) is EndpointGroupBase instance)
                {
                    instance.Map(builder);
                }
            }

            return builder;
        }

        public static IEndpointRouteBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler, string pattern = "")
        {
            handler.Method.ThrowIfAnonymous();

            builder.MapGet(pattern, handler)
                   .WithName(handler.Method.Name);

            return builder;
        }

        public static IEndpointRouteBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler, string pattern = "")
        {
            handler.Method.ThrowIfAnonymous();

            builder.MapPost(pattern, handler)
                   .WithName(handler.Method.Name);

            return builder;
        }

        public static IEndpointRouteBuilder MapPut(this IEndpointRouteBuilder builder, Delegate handler, string pattern = "")
        {
            handler.Method.ThrowIfAnonymous();

            builder.MapPut(pattern, handler)
                   .WithName(handler.Method.Name);

            return builder;
        }

        public static IEndpointRouteBuilder MapDelete(this IEndpointRouteBuilder builder, Delegate handler, string pattern)
        {
            handler.Method.ThrowIfAnonymous();

            builder.MapDelete(pattern, handler)
                   .WithName(handler.Method.Name);

            return builder;
        }

        private static void ThrowIfAnonymous(this MethodInfo method)
        {
            var invalidChars = new[] { '<', '>' };
            
            if (method.Name.Any(invalidChars.Contains))
                throw new ArgumentException("The endpoint name must be specified when using anonymous handlers.");
        }
    }   
}
