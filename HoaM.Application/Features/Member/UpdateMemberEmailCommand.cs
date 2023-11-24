using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record UpdateMemberEmailCommand(AssociationMemberId MemberId, EmailAddress EmailAddress) : ICommand, ICommandBinder<AssociationMember, AssociationMemberId>
    {
        public AssociationMember? Entity { get; set; }
    }

    public sealed class UpdateMemberEmailValidator : AbstractValidator<UpdateMemberEmailCommand>
    {
        public UpdateMemberEmailValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.MemberId).NotEmpty();

            RuleFor(command => command.EmailAddress).NotEmpty();

            RuleFor(command => command.Entity)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.AssociationMember.NotFound.Code)
                    .WithMessage(ApplicationErrors.AssociationMember.NotFound.Message)
                .Must(member => !member!.IsDeleted)
                    .WithErrorCode(ApplicationErrors.AssociationMember.AlreadyDeleted.Code)
                    .WithMessage(ApplicationErrors.AssociationMember.AlreadyDeleted.Message);
        }
    }

    internal sealed class UpdateMemberEmailHandler : ICommandHandler<UpdateMemberEmailCommand>
    {
        public Task<IResult> Handle(UpdateMemberEmailCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.WithEmailAddress(request.EmailAddress);

            return Results.Success();
        }
    }
}
