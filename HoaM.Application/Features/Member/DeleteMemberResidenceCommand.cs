using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record DeleteMemberResidenceCommand(AssociationMemberId MemberId) : ICommand, ICommandBinder<AssociationMember, AssociationMemberId>
    {
        public AssociationMember? Entity { get; set; }
    }

    public sealed class DeleteMemberResidenceValidator : AbstractValidator<DeleteMemberResidenceCommand>
    {
        public DeleteMemberResidenceValidator()
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
                    .WithMessage(ApplicationErrors.AssociationMember.AlreadyDeleted.Message)
                .Must(member => member!.Residence is not null)
                    .WithErrorCode(ApplicationErrors.Residence.NotFound.Code)
                    .WithMessage(ApplicationErrors.Residence.NotFound.Message);
        }
    }

    public sealed class DeleteMemberResidenceHandler : ICommandHandler<DeleteMemberResidenceCommand>
    {
        public Task<IResult> Handle(DeleteMemberResidenceCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.RemoveResidence();

            return Results.Success();
        }
    }
}
