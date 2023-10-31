using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record CreateCommitteeCommand(CommitteeName Name, DateOnly? DateEstablished = null) : ICommand<IResult> { }

    public sealed class CreateCommitteeValidator : AbstractValidator<CreateCommitteeCommand>
    {
        public CreateCommitteeValidator(IReadRepository<Committee> repository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Name)
                .NotEmpty()
                .MustAsync(async (name, cancellationToken) =>
                {
                    var spec = new CommitteeByNameSpec(name);
                    var committee = await repository.FirstOrDefaultAsync(spec, cancellationToken);

                    return committee is null;
                })
                .WithErrorCode(ApplicationErrors.Committee.DuplicateName.Code)
                .WithMessage(ApplicationErrors.Committee.DuplicateName.Message);
        }
    }

    internal sealed class CreateCommitteeCommandHandler : ICommandHandler<CreateCommitteeCommand, IResult>
    {
        private readonly IRepository<Committee> _committeeRepository;

        public CreateCommitteeCommandHandler(IRepository<Committee> committeeRepository)
        {
            _committeeRepository = committeeRepository;
        }

        public async Task<IResult> Handle(CreateCommitteeCommand request, CancellationToken cancellationToken)
        {
            await _committeeRepository.AddAsync(Committee.Create(request.Name, request.DateEstablished), cancellationToken);

            return Results.Success();
        }
    }
}
