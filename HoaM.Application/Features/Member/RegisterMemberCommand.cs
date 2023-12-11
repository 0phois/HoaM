using FluentValidation;
using HoaM.Application.Common;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record RegisterMemberCommand(FirstName FirstName, LastName LastName, EmailAddress Email) : ICommand<AssociationMember> { }

    public sealed class RegisterMemberValidator : AbstractValidator<RegisterMemberCommand>
    {
        public RegisterMemberValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.FirstName).NotEmpty();

            RuleFor(command => command.LastName).NotEmpty();

            RuleFor(command => command.Email).NotEmpty();
        }
    }

    public sealed class RegisterMemberHandler(IAssociationMemberRepository repository) : ICommandHandler<RegisterMemberCommand, AssociationMember>
    {
        public Task<IResult<AssociationMember>> Handle(RegisterMemberCommand request, CancellationToken cancellationToken)
        {
            var member = AssociationMember.Create(request.FirstName, request.LastName).WithEmailAddress(request.Email);

            repository.Insert(member);

            return Results.Success(member);
        }
    }
}
