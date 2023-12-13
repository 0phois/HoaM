using Microsoft.AspNetCore.Routing;

namespace HoaM.API
{
    public abstract class EndpointGroupBase
    {
        public abstract void Map(IEndpointRouteBuilder builder);
    }
}
