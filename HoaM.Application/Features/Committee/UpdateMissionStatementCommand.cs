using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record UpdateMissionStatementCommand(CommitteeId CommitteeId, MissionStatement Statement) : ICommand<IResult>, ICommandBinder<Committee, CommitteeId>
    {
        public Committee? Entity { get; set; }
    }

    public sealed class UpdateMissionStatementValidator : AbstractValidator<UpdateMissionStatementCommand>
    {
        public UpdateMissionStatementValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.CommitteeId);

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

            RuleFor(command => command.Statement).NotEmpty();
        }
    }

    internal sealed class UpdateMissionStatementHandler : ICommandHandler<UpdateMissionStatementCommand, IResult>
    {
        public Task<IResult> Handle(UpdateMissionStatementCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.WithMissionStatement(request.Statement);

            return Results.Success();
        }
    }
}
