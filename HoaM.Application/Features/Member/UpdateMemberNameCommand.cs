using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record UpdateMemberNameCommand(AssociationMemberId MemberId, FirstName FirstName, LastName LastName) : ICommand<IResult>, ICommandBinder<AssociationMember, AssociationMemberId>
    {
        public AssociationMember? Entity { get; set; }
    }

    public sealed class UpdateMemberNameValidator : AbstractValidator<UpdateMemberNameCommand>
    {
        public UpdateMemberNameValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.MemberId).NotEmpty();

            RuleFor(command => command.FirstName).NotEmpty();

            RuleFor(command => command.LastName).NotEmpty();

            RuleFor(command => command.Entity)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.AssociationMember.NotFound.Code)
                    .WithMessage(ApplicationErrors.AssociationMember.NotFound.Message)
                .Must(member => !member!.IsDeleted)
                    .WithErrorCode(ApplicationErrors.AssociationMember.AlreadyDeleted.Code)
                    .WithMessage(ApplicationErrors.AssociationMember.AlreadyDeleted.Message);
        }
    }

    internal sealed class UpdateMemberNameHandler : ICommandHandler<UpdateMemberNameCommand, IResult>
    {
        public Task<IResult> Handle(UpdateMemberNameCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.EditFirstName(request.FirstName);
            request.Entity!.EditLastName(request.LastName);

            return Results.Success();
        }
    }
}
