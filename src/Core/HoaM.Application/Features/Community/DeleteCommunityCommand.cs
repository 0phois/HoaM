﻿using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record DeleteCommunityCommand(CommunityId CommunityId) : ICommand<IResult>, ICommunityBinder
    {
        public CommunityId Id => CommunityId;

        public Community? Entity { get; set; }
    }

    public sealed class DeleteCommunityValidator : AbstractValidator<DeleteCommunityCommand>
    {
        public DeleteCommunityValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.CommunityId).NotEmpty();

            RuleFor(command => command.Entity)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.Community.NotFound.Code)
                    .WithMessage(ApplicationErrors.Community.NotFound.Message);
        }
    }

    public sealed class DeleteCommunityHandler(ICommunityRepository repository) : ICommandHandler<DeleteCommunityCommand, IResult>
    {
        public Task<IResult> Handle(DeleteCommunityCommand request, CancellationToken cancellationToken)
        {
            repository.Remove(request.Entity!);

            return Results.Success();
        }
    }
}
