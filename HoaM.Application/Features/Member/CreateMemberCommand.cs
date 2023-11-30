using FluentValidation;
using HoaM.Application.Common;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;
using TanvirArjel.EFCore.GenericRepository;

namespace HoaM.Application.Features
{
    public sealed record CreateMemberCommand(FirstName FirstName, LastName LastName) : ICommand<AssociationMember> { }

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

    public sealed class CreateAssociationMemberHandler(IRepository repository) : ICommandHandler<CreateMemberCommand, AssociationMember>
    {
        public async Task<IResult<AssociationMember>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = AssociationMember.Create(request.FirstName, request.LastName);
            
            await repository.AddAsync(member, cancellationToken);

            return Results.Success(member);
        }
    }
}
