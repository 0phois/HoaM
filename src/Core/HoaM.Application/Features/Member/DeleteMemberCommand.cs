﻿using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record DeleteMemberCommand(AssociationMemberId MemberId) : ICommand<IResult>, IMemberBinder
    {
        public AssociationMemberId Id => MemberId;
        public AssociationMember? Entity { get; set; }
    }

    public sealed class DeleteMemberValidator : AbstractValidator<DeleteMemberCommand>
    {
        public DeleteMemberValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.MemberId).NotEmpty();

            RuleFor(command => command.Entity)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.AssociationMember.NotFound.Code)
                    .WithMessage(ApplicationErrors.AssociationMember.NotFound.Message)
                .Must(member => !member!.IsDeleted)
                    .WithErrorCode(ApplicationErrors.AssociationMember.AlreadyDeleted.Code)
                    .WithMessage(ApplicationErrors.AssociationMember.AlreadyDeleted.Message);
        }
    }

    public sealed class DeleteMemberHandler(IAssociationMemberRepository repository) : ICommandHandler<DeleteMemberCommand, IResult>
    {
        public Task<IResult> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            repository.Remove(request.Entity!);

            return Results.Success();
        }
    }
}
