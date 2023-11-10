using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Common.Contracts;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record UpdateCommitteeDetailsCommand(CommitteeId CommitteeId, params Note[] Details) : ICommand<IResult>, ICommandBinder<Committee, CommitteeId>
    {
        public Committee? Entity { get; set; }
    }

    public sealed class UpdateCommitteeDetailsValidator : AbstractValidator<UpdateCommitteeDetailsCommand>
    {
        public UpdateCommitteeDetailsValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.CommitteeId).NotEmpty();

            RuleFor(command => command.Entity)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.Committee.NotFound.Code)
                    .WithMessage(ApplicationErrors.Committee.NotFound.Message)
                .Must(committee => committee!.IsDeleted == false)
                    .WithErrorCode(ApplicationErrors.Committee.AlreadyDeleted.Code)
                    .WithMessage(ApplicationErrors.Committee.AlreadyDeleted.Message)
                .Must(committee => committee!.IsDissolved == false)
                    .WithErrorCode(ApplicationErrors.Committee.AlreadyDissolved.Code)
                    .WithMessage(ApplicationErrors.Committee.AlreadyDissolved.Message);

            RuleFor(command => command.Details).NotEmpty();
        }
    }

    internal sealed class UpdateCommitteeDetailsHandler : ICommandHandler<UpdateCommitteeDetailsCommand, IResult>
    {
        public Task<IResult> Handle(UpdateCommitteeDetailsCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.WithAdditionalDetails(request.Details);

            return Results.Success();
        }
    }
}
