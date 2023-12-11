using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record DeleteCommitteeCommand(CommitteeId CommitteeId) : ICommand, ICommitteeBinder
    {
        public CommitteeId Id => CommitteeId;
        public Committee? Entity { get; set; }
    }

    public sealed class DeleteCommitteeValidator : AbstractValidator<DeleteCommitteeCommand>
    {
        public DeleteCommitteeValidator()
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
        }
    }

    public sealed class DeleteCommitteeHandler(ICommitteeRepository repository) : ICommandHandler<DeleteCommitteeCommand>
    {
        public Task<IResult> Handle(DeleteCommitteeCommand request, CancellationToken cancellationToken)
        {
            repository.Remove(request.Entity!);

            return Results.Success();
        }
    }
}
