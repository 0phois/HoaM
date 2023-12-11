using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record DeletePhoneNumberCommand(AssociationMemberId MemberId, PhoneNumber PhoneNumber) : ICommand, IMemberBinder
    {
        public AssociationMemberId Id => MemberId;
        public AssociationMember? Entity { get; set; }
    }

    public sealed class DeletePhoneNumberValidator : AbstractValidator<DeletePhoneNumberCommand>
    {
        public DeletePhoneNumberValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.MemberId).NotEmpty();

            RuleFor(command => command.PhoneNumber).NotEmpty();

            RuleFor(command => command.Entity)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.AssociationMember.NotFound.Code)
                    .WithMessage(ApplicationErrors.AssociationMember.NotFound.Message)
                .Must(member => !member!.IsDeleted)
                    .WithErrorCode(ApplicationErrors.AssociationMember.AlreadyDeleted.Code)
                    .WithMessage(ApplicationErrors.AssociationMember.AlreadyDeleted.Message)
                .Must((cmd, member) => member!.PhoneNumbers.Contains(cmd.PhoneNumber))
                    .WithErrorCode(ApplicationErrors.PhoneNumber.NotFound.Code)
                    .WithMessage(ApplicationErrors.PhoneNumber.NotFound.Message);
        }
    }

    public sealed class DeletePhoneNumberHandler : ICommandHandler<DeletePhoneNumberCommand>
    {
        public Task<IResult> Handle(DeletePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.RemovePhoneNumber(request.PhoneNumber);

            return Results.Success();
        }
    }
}
