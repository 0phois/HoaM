using Microsoft.AspNetCore.Routing;

namespace HoaM.API.Features
{
    public abstract class EndpointGroupBase
    {
        public abstract void Map(IEndpointRouteBuilder builder);
    }
}
