using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record CreateCommitteeCommand(CommitteeName Name, DateOnly? DateEstablished = null) : ICommand<Committee> { }

    public sealed class CreateCommitteeValidator : AbstractValidator<CreateCommitteeCommand>
    {
        public CreateCommitteeValidator(IReadRepository<Committee> repository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Name)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.Committee.NotFound.Code)
                    .WithMessage(ApplicationErrors.Committee.NotFound.Message)
                .MustAsync(async (name, cancellationToken) =>
                {
                    var spec = new CommitteeByNameSpec(name);
                    var committee = await repository.FirstOrDefaultAsync(spec, cancellationToken);

                    return committee is null;
                }).WithErrorCode(ApplicationErrors.Committee.DuplicateName.Code)
                  .WithMessage(ApplicationErrors.Committee.DuplicateName.Message);
        }
    }

    internal sealed class CreateCommitteeCommandHandler(IRepository<Committee> committeeRepository) : ICommandHandler<CreateCommitteeCommand, Committee>
    {
        public async Task<IResult<Committee>> Handle(CreateCommitteeCommand request, CancellationToken cancellationToken)
        {
            var committee = await committeeRepository.AddAsync(Committee.Create(request.Name, request.DateEstablished), cancellationToken);

            return Results.Success(committee);
        }
    }
}
