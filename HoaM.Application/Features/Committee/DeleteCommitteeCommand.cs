using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record DeleteCommitteeCommand(CommitteeId CommitteeId) : ICommand, ICommandBinder<Committee, CommitteeId>
    {
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

    public sealed class DeleteCommitteeHandler : ICommandHandler<DeleteCommitteeCommand>
    {
        private readonly IRepository<Committee> _repository;

        public DeleteCommitteeHandler(IRepository<Committee> repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(DeleteCommitteeCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Entity!, cancellationToken);

            return Results.Success();
        }
    }
}
