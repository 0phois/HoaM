using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;
using TanvirArjel.EFCore.GenericRepository;

namespace HoaM.Application.Features
{
    public sealed record CreateCommitteeCommand(CommitteeName Name, DateOnly? DateEstablished = null) : ICommand<Committee> { }

    public sealed class CreateCommitteeValidator : AbstractValidator<CreateCommitteeCommand>
    {
        public CreateCommitteeValidator(IRepository repository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Name)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.Committee.NotFound.Code)
                    .WithMessage(ApplicationErrors.Committee.NotFound.Message)
                .MustAsync(async (name, cancellationToken) =>
                {
                    var spec = new CommitteeByNameSpec(name);
                    var committee = await repository.GetAsync(spec, true, cancellationToken);

                    return committee is null;
                }).WithErrorCode(ApplicationErrors.Committee.DuplicateName.Code)
                  .WithMessage(ApplicationErrors.Committee.DuplicateName.Message);
        }
    }

    public sealed class CreateCommitteeCommandHandler(IRepository repository) : ICommandHandler<CreateCommitteeCommand, Committee>
    {
        public async Task<IResult<Committee>> Handle(CreateCommitteeCommand request, CancellationToken cancellationToken)
        {
            var committee = Committee.Create(request.Name, request.DateEstablished);
            
            await repository.AddAsync(committee, cancellationToken);

            return Results.Success(committee);
        }
    }
}
