﻿using HoaM.Application;
using HoaM.Application.Common;
using HoaM.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace HoaM.API
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

        public static async Task<CommunityId> CreateCommunity([FromServices] ICommandService commandService, CommunityName name)
        {
            var result = await commandService.ExecuteAsync<CreateCommunityCommand, CommunityId>(new CreateCommunityCommand(name));

            return result.Value;
        }

        public static async Task<IResult> UpdateCommunityName([FromServices] ICommandService commandService, CommunityId id, CommunityName newName)
        {
            var result = await commandService.ExecuteAsync(new UpdateCommunityNameCommand(id, newName));

            return Results.NoContent();
        }

        public static async Task<IResult> DeleteCommunity([FromServices] ICommandService commandService, CommunityId id)
        {
            var result = await commandService.ExecuteAsync(new DeleteCommunityCommand(id));

            return Results.NoContent();
        }
    }
}
