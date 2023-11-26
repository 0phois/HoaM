using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record DissolveCommitteeCommand(CommitteeId CommitteeId) : ICommand, ICommandBinder<Committee, CommitteeId>
    {
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

    internal sealed class DissolveCommitteeHandler : ICommandHandler<DissolveCommitteeCommand>
    {
        private readonly ISystemClock _clock;

        public DissolveCommitteeHandler(ISystemClock systemClock)
        {
            _clock = systemClock;
        }

        public Task<IResult> Handle(DissolveCommitteeCommand request, CancellationToken cancellationToken)
        {
            var dissolved = request.Entity!.TryDissolve(_clock);

            return dissolved ? Results.Success() : Results.Failed("An error occurred while attempting to dissolve the committee.");
        }
    }
}
