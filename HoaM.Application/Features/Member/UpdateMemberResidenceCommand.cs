using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record UpdateMemberResidenceCommand(AssociationMemberId MemberId, Residence Residence) : ICommand, ICommandBinder<AssociationMember, AssociationMemberId>
    {
        public AssociationMember? Entity { get; set; }
    }

    public sealed class UpdateMemberResidenceValidator : AbstractValidator<UpdateMemberResidenceCommand>
    {
        public UpdateMemberResidenceValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.MemberId).NotEmpty();

            RuleFor(command => command.Residence)
                .NotEmpty()
                .Must(residence => residence.DeletionDate is null)
                    .WithErrorCode(ApplicationErrors.Residence.AlreadyDeleted.Code)
                    .WithMessage(ApplicationErrors.Residence.AlreadyDeleted.Message);

            RuleFor(command => command.Entity)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.AssociationMember.NotFound.Code)
                    .WithMessage(ApplicationErrors.AssociationMember.NotFound.Message)
                .Must(member => !member!.IsDeleted)
                    .WithErrorCode(ApplicationErrors.AssociationMember.AlreadyDeleted.Code)
                    .WithMessage(ApplicationErrors.AssociationMember.AlreadyDeleted.Message);
        }
    }

    public sealed class UpdateMemberResidenceHanler : ICommandHandler<UpdateMemberResidenceCommand>
    {
        public Task<IResult> Handle(UpdateMemberResidenceCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.WithResidence(request.Residence);

            return Results.Success();
        }
    }
}
