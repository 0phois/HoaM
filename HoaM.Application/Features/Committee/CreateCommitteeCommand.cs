using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record CreateCommitteeCommand(CommitteeName Name, DateOnly? DateEstablished = null) : ICommand<IResult<Committee>> { }

    public sealed class CreateCommitteeValidator : AbstractValidator<CreateCommitteeCommand>
    {
        public CreateCommitteeValidator(ICommitteeRepository repository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Name)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.Committee.NotFound.Code)
                    .WithMessage(ApplicationErrors.Committee.NotFound.Message)
                .MustAsync(repository.IsNameUniqueAsync)
                    .WithErrorCode(ApplicationErrors.Committee.DuplicateName.Code)
                    .WithMessage(ApplicationErrors.Committee.DuplicateName.Message);
        }
    }

    public sealed class CreateCommitteeCommandHandler(ICommitteeRepository repository) : ICommandHandler<CreateCommitteeCommand, IResult<Committee>>
    {
        public Task<IResult<Committee>> Handle(CreateCommitteeCommand request, CancellationToken cancellationToken)
        {
            var committee = Committee.Create(request.Name, request.DateEstablished);

            repository.Insert(committee);

            return Results.Success(committee);
        }
    }
}
