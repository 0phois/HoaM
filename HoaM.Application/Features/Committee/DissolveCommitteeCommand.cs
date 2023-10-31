using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record DissolveCommitteeCommand(Committee Committee) : ICommand<IResult> { }

    public sealed class DissolveCommitteeValidator : AbstractValidator<DissolveCommitteeCommand> 
    {
        public DissolveCommitteeValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Committee).NotEmpty();

            RuleFor(command => command.Committee.DeletionDate).Null()
                .WithErrorCode(ApplicationErrors.Committee.AlreadyDeleted.Code)
                .WithMessage(ApplicationErrors.Committee.AlreadyDeleted.Message);

            RuleFor(command => command.Committee.Dissolved).Null()
                .WithErrorCode(ApplicationErrors.Committee.AlreadyDissolved.Code)
                .WithMessage(ApplicationErrors.Committee.AlreadyDissolved.Message);
        }
    }

    internal sealed class DissolveCommitteeHandler : ICommandHandler<DissolveCommitteeCommand, IResult>
    {
        private readonly ISystemClock _clock;

        public DissolveCommitteeHandler(ISystemClock systemClock)
        {
            _clock = systemClock;
        }

        public Task<IResult> Handle(DissolveCommitteeCommand request, CancellationToken cancellationToken)
        {
            var dissolved = request.Committee.TryDissolve(_clock);

            return dissolved ? Results.Success() : Results.Failed("An error occurred while attempting to dissolve the committee.");
        }
    }
}
