using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record UpdateCommitteeNameCommand(CommitteeId CommitteeId, CommitteeName NewName) : ICommand<IResult>, ICommitteeBinder
    {
        public CommitteeId Id => CommitteeId;
        public Committee? Entity { get; set; }
    }

    public sealed class UpdateCommitteeNameValidator : AbstractValidator<UpdateCommitteeNameCommand>
    {
        public UpdateCommitteeNameValidator(ICommitteeRepository repository)
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
                .MustAsync(repository.IsNameUniqueAsync)
                .WithErrorCode(ApplicationErrors.Committee.DuplicateName.Code)
                .WithMessage(ApplicationErrors.Committee.DuplicateName.Message)
                .When(command => !command.Entity!.Name.Value.Equals(command.NewName.Value, StringComparison.OrdinalIgnoreCase));
        }
    }

    public sealed class UpdateCommitteeNameCommandHandler : ICommandHandler<UpdateCommitteeNameCommand, IResult>
    {
        public Task<IResult> Handle(UpdateCommitteeNameCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.EditName(request.NewName);

            return Results.Success();
        }
    }
}
