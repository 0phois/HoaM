using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record DissolveCommitteeCommand(CommitteeId CommitteeId) : ICommand<IResult>, ICommitteeBinder
    {
        public CommitteeId Id => CommitteeId;
        public Committee? Entity { get; set; }
    }

    public sealed class DissolveCommitteeValidator : AbstractValidator<DissolveCommitteeCommand>
    {
        public DissolveCommitteeValidator()
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
        }
    }

    public sealed class DissolveCommitteeHandler(TimeProvider systemClock) : ICommandHandler<DissolveCommitteeCommand, IResult>
    {
        private readonly TimeProvider _clock = systemClock;

        public Task<IResult> Handle(DissolveCommitteeCommand request, CancellationToken cancellationToken)
        {
            var dissolved = request.Entity!.TryDissolve(_clock);

            return dissolved ? Results.Success() : Results.Failed("An error occurred while attempting to dissolve the committee.");
        }
    }
}
