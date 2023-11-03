using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record AppendCommitteeDetailsCommand(Committee Committee, params Note[] Details) : ICommand<IResult> { }

    public sealed class AppendCommitteeDetailsValidator : AbstractValidator<AppendCommitteeDetailsCommand>
    {
        public AppendCommitteeDetailsValidator(IReadRepository<Committee> repository)
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

            RuleFor(command => command.Details).NotEmpty();
        }
    }

    internal sealed class AppendCommitteeDetailsHandler : ICommandHandler<AppendCommitteeDetailsCommand, IResult>
    {
        public Task<IResult> Handle(AppendCommitteeDetailsCommand request, CancellationToken cancellationToken)
        {
            request.Committee.AppendAdditionalDetails(request.Details);

            return Results.Success();
        }
    }
}
