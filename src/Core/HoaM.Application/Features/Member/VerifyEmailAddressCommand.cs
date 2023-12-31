﻿using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record VerifyEmailAddressCommand(AssociationMemberId MemberId) : ICommand<IResult>, IMemberBinder
    {
        public AssociationMemberId Id => MemberId;
        public AssociationMember? Entity { get; set; }
    }

    public sealed class VerifyEmailAddressValidator : AbstractValidator<VerifyEmailAddressCommand>
    {
        public VerifyEmailAddressValidator()
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

            RuleFor(command => command.Entity!.Email)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.Email.NotFound.Code)
                    .WithMessage(ApplicationErrors.Email.NotFound.Message)
                .Must(email => !email!.IsVerified)
                    .WithErrorCode(ApplicationErrors.Email.AlreadyVerified.Code)
                    .WithMessage(ApplicationErrors.Email.AlreadyVerified.Message);
        }
    }

    public sealed class VerifyEmailAddressHandler : ICommandHandler<VerifyEmailAddressCommand, IResult>
    {
        public Task<IResult> Handle(VerifyEmailAddressCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.Email!.Verify();

            return Results.Success();
        }
    }
}
