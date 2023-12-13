using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record UpdatePhoneNumbersCommand(AssociationMemberId MemberId, params PhoneNumber[] PhoneNumbers) : ICommand<IResult>, IMemberBinder
    {
        public AssociationMemberId Id => MemberId;
        public AssociationMember? Entity { get; set; }
    }

    public sealed class UpdatePhoneNumbersValidator : AbstractValidator<UpdatePhoneNumbersCommand>
    {
        private const int MaxNumbers = 3;

        public UpdatePhoneNumbersValidator()
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

            RuleFor(command => command.PhoneNumbers)
                .NotEmpty()
                .Must(numbers => numbers.Length <= MaxNumbers)
                    .WithErrorCode(ApplicationErrors.PhoneNumber.LimitExceeded.Code)
                    .WithMessage(ApplicationErrors.PhoneNumber.LimitExceeded.Message)
                .Must(numbers => numbers.DistinctBy(number => number.Type).Count() == numbers.Length)
                    .WithErrorCode(ApplicationErrors.PhoneNumber.DuplicateTypeFound.Code)
                    .WithMessage(ApplicationErrors.PhoneNumber.DuplicateTypeFound.Message);


        }
    }

    public sealed class UpdatePhoneNumbersHandler : ICommandHandler<UpdatePhoneNumbersCommand, IResult>
    {
        public Task<IResult> Handle(UpdatePhoneNumbersCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.WithPhoneNumbers(request.PhoneNumbers);

            return Results.Success();
        }
    }
}
