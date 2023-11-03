using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record DeleteCommitteeCommand(Committee Committee) : ICommand<IResult> { }

    public sealed class DeleteCommitteeValidator : AbstractValidator<DeleteCommitteeCommand> 
    {
        public DeleteCommitteeValidator(IReadRepository<Committee> repository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Committee)
                .NotEmpty()
                .MustAsync(async (request, cancellationToken) => 
                {
                    var committee = await repository.GetByIdAsync(request.Id, cancellationToken);

                    return committee is not null && !committee.IsDeleted;
                })
                .WithErrorCode(ApplicationErrors.Committee.NotFound.Code)
                .WithMessage(ApplicationErrors.Committee.NotFound.Message);
        }
    }

    internal sealed class DeleteCommitteeHandler : ICommandHandler<DeleteCommitteeCommand, IResult>
    {
        private readonly IRepository<Committee> _repository;

        public DeleteCommitteeHandler(IRepository<Committee> repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(DeleteCommitteeCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Committee, cancellationToken);

            return Results.Success();
        }
    }
}
