using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record AddPhoneNumberCommand(AssociationMember Member, PhoneNumber PhoneNumber) : ICommand<IResult> { }

    public sealed class AddPhoneNumberValidator : AbstractValidator<AddPhoneNumberCommand> 
    {
        public AddPhoneNumberValidator(IReadRepository<AssociationMember> repository)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.PhoneNumber).NotEmpty();

            RuleFor(command => command.Member)
                .NotEmpty()
                .MustAsync(async (cmd, member, cancellationToken) =>
                {
                    var user = await repository.GetByIdAsync(member.Id, cancellationToken);

                    return user is not null && user.PhoneNumbers.Exists(x => x.Type == cmd.PhoneNumber.Type);
                })
                .WithErrorCode(ApplicationErrors.AssociationMember.DuplicatePhone.Code)
                .WithMessage(ApplicationErrors.AssociationMember.DuplicatePhone.Message);
        }
    }

    internal sealed class AddPhoneNumberHandler : ICommandHandler<AddPhoneNumberCommand, IResult>
    {
        public Task<IResult> Handle(AddPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            request.Member.AddPhoneNumber(request.PhoneNumber);

            return Results.Success();
        }
    }
}
