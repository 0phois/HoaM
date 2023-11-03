using FluentValidation;
using HoaM.Application.Common;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record CreateMemberCommand(FirstName FirstName, LastName LastName) : ICommand<IResult<AssociationMember>> { }

    public sealed class CreateAssociationMemberValidator : AbstractValidator<CreateMemberCommand> 
    {
        public CreateAssociationMemberValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.FirstName).NotEmpty();

            RuleFor(command => command.LastName).NotEmpty();
        }
    }

    internal sealed class CreateAssociationMemberHandler : ICommandHandler<CreateMemberCommand, IResult<AssociationMember>>
    {
        private readonly IRepository<AssociationMember> _repository;

        public CreateAssociationMemberHandler(IRepository<AssociationMember> repository)
        {
            _repository = repository;
        }

        public async Task<IResult<AssociationMember>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _repository.AddAsync(AssociationMember.Create(request.FirstName, request.LastName), cancellationToken);

            return Results.Success(member);
        }
    }
}
