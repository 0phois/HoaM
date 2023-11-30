using FluentValidation;
using HoaM.Application.Common;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;
using TanvirArjel.EFCore.GenericRepository;

namespace HoaM.Application.Features
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

    public sealed class RegisterMemberHandler(IRepository repository) : ICommandHandler<RegisterMemberCommand, AssociationMember>
    {
        public async Task<IResult<AssociationMember>> Handle(RegisterMemberCommand request, CancellationToken cancellationToken)
        {
            var member = AssociationMember.Create(request.FirstName, request.LastName).WithEmailAddress(request.Email);

            await repository.AddAsync(member, cancellationToken);

            return Results.Success(member);
        }
    }
}
