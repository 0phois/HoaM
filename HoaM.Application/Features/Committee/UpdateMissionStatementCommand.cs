using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record UpdateMissionStatementCommand(Committee Committee, MissionStatement Statement) : ICommand<IResult> { }

    public sealed class UpdateMissionStatementValidator : AbstractValidator<UpdateMissionStatementCommand>
    {
        public UpdateMissionStatementValidator(IReadRepository<Committee> repository)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Committee)
                .NotEmpty()
                .MustAsync(async (request, cancellationToken) =>
                {
                    var committee = await repository.GetByIdAsync(request.Id, cancellationToken);

                    return committee is not null && committee.IsActive;
                })
                .WithErrorCode(ApplicationErrors.Committee.NotFound.Code)
                .WithMessage(ApplicationErrors.Committee.NotFound.Message);

            RuleFor(command => command.Statement).NotEmpty();
        }
    }

    internal sealed class UpdateMissionStatementHandler : ICommandHandler<UpdateMissionStatementCommand, IResult>
    {
        public Task<IResult> Handle(UpdateMissionStatementCommand request, CancellationToken cancellationToken)
        {
            request.Committee.WithMissionStatement(request.Statement);

            return Results.Success();
        }
    }
}
