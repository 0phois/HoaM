﻿using FluentValidation;
using HoaM.Application.Common;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

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

    public sealed class CreateAssociationMemberHandler(IMemberRepository repository) : ICommandHandler<CreateMemberCommand, AssociationMember>
    {
        public Task<IResult<AssociationMember>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = AssociationMember.Create(request.FirstName, request.LastName);
            
            repository.Insert(member);

            return Results.Success(member);
        }
    }
}
