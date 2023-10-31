using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record UpdateCommitteeNameCommand(Committee Committee, CommitteeName NewName) : ICommand<IResult> { }

    public sealed class UpdateCommitteeNameValidator : AbstractValidator<UpdateCommitteeNameCommand>
    {
        public UpdateCommitteeNameValidator(IReadRepository<Committee> repository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.NewName)
                .NotEmpty()
                .MustAsync(async (newName, cancellationToken) =>
                {
                    var spec = new CommitteeByNameSpec(newName);
                    var committee = await repository.FirstOrDefaultAsync(spec, cancellationToken);

                    return committee is null;
                })
                .When(command => command.Committee.Name != command.NewName)
                .WithErrorCode(ApplicationErrors.Committee.DuplicateName.Code)
                .WithMessage(ApplicationErrors.Committee.DuplicateName.Message);
        }
    }

    internal sealed class UpdateCommitteeNameCommandHandler : ICommandHandler<UpdateCommitteeNameCommand, IResult>
    {
        public Task<IResult> Handle(UpdateCommitteeNameCommand request, CancellationToken cancellationToken)
        {
            request.Committee.EditName(request.NewName);

            return Results.Success();
        }
    }
}
