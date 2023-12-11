using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record AddPhoneNumberCommand(AssociationMemberId MemberId, PhoneNumber PhoneNumber) : ICommand, IMemberBinder
    {
        public AssociationMemberId Id => MemberId;
        public AssociationMember? Entity { get; set; }
    }

    public sealed class AddPhoneNumberValidator : AbstractValidator<AddPhoneNumberCommand>
    {
        private const int MaxNumbers = 3;

        public AddPhoneNumberValidator()
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
                .Must((cmd, member) => member!.PhoneNumbers.Any((x) => x.Type == cmd.PhoneNumber.Type))
                    .WithErrorCode(ApplicationErrors.AssociationMember.DuplicatePhone.Code)
                    .WithMessage(ApplicationErrors.AssociationMember.DuplicatePhone.Message)
                .Must(member => member!.PhoneNumbers.Count < MaxNumbers)
                    .WithErrorCode(ApplicationErrors.PhoneNumber.LimitReached.Code)
                    .WithMessage(ApplicationErrors.PhoneNumber.LimitReached.Message);
        }
    }

    public sealed class AddPhoneNumberHandler : ICommandHandler<AddPhoneNumberCommand>
    {
        public Task<IResult> Handle(AddPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.AddPhoneNumber(request.PhoneNumber);

            return Results.Success();
        }
    }
}
