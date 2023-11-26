using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record UpdateCommitteeNameCommand(CommitteeId CommitteeId, CommitteeName NewName) : ICommand, ICommandBinder<Committee, CommitteeId>
    {
        public Committee? Entity { get; set; }
    }

    public sealed class UpdateCommitteeNameValidator : AbstractValidator<UpdateCommitteeNameCommand>
    {
        public UpdateCommitteeNameValidator(IReadRepository<Committee> repository)
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
                    .WithMessage(ApplicationErrors.Committee.AlreadyDeleted.Message);

            RuleFor(command => command.NewName)
                .NotEmpty()
                .MustAsync(async (newName, cancellationToken) =>
                {
                    var spec = new CommitteeByNameSpec(newName);
                    var committee = await repository.FirstOrDefaultAsync(spec, cancellationToken);

                    return committee is null;
                })
                .WithErrorCode(ApplicationErrors.Committee.DuplicateName.Code)
                .WithMessage(ApplicationErrors.Committee.DuplicateName.Message)
                .When(command => !command.Entity!.Name.Value.Equals(command.NewName.Value, StringComparison.OrdinalIgnoreCase));
        }
    }

    public sealed class UpdateCommitteeNameCommandHandler : ICommandHandler<UpdateCommitteeNameCommand>
    {
        public Task<IResult> Handle(UpdateCommitteeNameCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.EditName(request.NewName);

            return Results.Success();
        }
    }
}
