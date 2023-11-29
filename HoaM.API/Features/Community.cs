using HoaM.Application.Common;
using HoaM.Application.Features;
using HoaM.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HoaM.API.Features
{
    public class Community : EndpointGroupBase
    {
        public override void Map(IEndpointRouteBuilder builder)
        {
            builder.MapGroup(this)
                   .MapPost(CreateCommunity)
                   .MapPut(UpdateCommunityName, "{id}")
                   .MapDelete(DeleteCommunity, "{id}");
        }

        public async Task<CommunityId> CreateCommunity([FromServices] ICommandService commandService, CommunityName name)
        {
            return await commandService.ExecuteAsync(new CreateCommunityCommand(name));
        }

        public async Task<IResult> UpdateCommunityName([FromServices] ICommandService commandService, CommunityId id, CommunityName newName)
        {
            await commandService.ExecuteAsync(new UpdateCommunityNameCommand(id, newName));

            return Results.NoContent();
        }

        public async Task<IResult> DeleteCommunity([FromServices] ICommandService commandService, CommunityId id)
        {
            await commandService.ExecuteAsync(new DeleteCommunityCommand(id));

            return Results.NoContent();
        }
    }
}
