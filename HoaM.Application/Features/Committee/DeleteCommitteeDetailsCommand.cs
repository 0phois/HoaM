using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record DeleteCommitteeDetailsCommand(CommitteeId CommitteeId) : ICommand<IResult>, ICommitteeBinder
    {
        public CommitteeId Id => CommitteeId;
        public Committee? Entity { get; set; }
    }

    public sealed class DeleteCommitteeDetailsValidator : AbstractValidator<DeleteCommitteeCommand>
    {
        public DeleteCommitteeDetailsValidator()
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

    public sealed class DeleteCommitteeDetailsHandler : ICommandHandler<DeleteCommitteeDetailsCommand, IResult>
    {
        public Task<IResult> Handle(DeleteCommitteeDetailsCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.RemoveDetails();

            return Results.Success();
        }
    }
}
