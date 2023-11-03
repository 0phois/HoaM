using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record DeleteCommitteeDetailsCommand(Committee Committee) : ICommand<IResult> { }

    public sealed class DeleteCommitteeDetailsValidator : AbstractValidator<DeleteCommitteeCommand> 
    {
        public DeleteCommitteeDetailsValidator(IReadRepository<Committee> repository)
        {
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
        }
    }

    internal sealed class DeleteCommitteeDetailsHandler : ICommandHandler<DeleteCommitteeDetailsCommand, IResult>
    {
        public Task<IResult> Handle(DeleteCommitteeDetailsCommand request, CancellationToken cancellationToken)
        {
            request.Committee.RemoveDetails();

            return Results.Success();
        }
    }
}
