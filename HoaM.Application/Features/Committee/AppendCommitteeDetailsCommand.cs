using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record AppendCommitteeDetailsCommand(CommitteeId CommitteeId, params Note[] Details) : ICommand<IResult>, ICommitteeBinder
    {
        public CommitteeId Id => CommitteeId;
        public Committee? Entity { get; set; }
    }

    public sealed class AppendCommitteeDetailsValidator : AbstractValidator<AppendCommitteeDetailsCommand>
    {
        public AppendCommitteeDetailsValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.CommitteeId).NotEmpty();

            RuleFor(command => command.Entity)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.Committee.NotFound.Code)
                    .WithMessage(ApplicationErrors.Committee.NotFound.Message)
                .Must(committee => !committee!.IsDeleted)
                    .WithErrorCode(ApplicationErrors.Committee.AlreadyDeleted.Code)
                    .WithMessage(ApplicationErrors.Committee.AlreadyDeleted.Message)
                .Must(committee => !committee!.IsDissolved)
                    .WithErrorCode(ApplicationErrors.Committee.AlreadyDissolved.Code)
                    .WithMessage(ApplicationErrors.Committee.AlreadyDissolved.Message);

            RuleFor(command => command.Details).NotEmpty();
        }
    }

    public sealed class AppendCommitteeDetailsHandler : ICommandHandler<AppendCommitteeDetailsCommand, IResult>
    {
        public Task<IResult> Handle(AppendCommitteeDetailsCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.AppendAdditionalDetails(request.Details);

            return Results.Success();
        }
    }
}
