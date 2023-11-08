﻿using FluentValidation;
using HoaM.Application.Common;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record RegisterMemberCommand(FirstName FirstName, LastName LastName, EmailAddress Email) : ICommand<IResult<AssociationMember>> { }

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

    internal sealed class RegisterMemberHandler : ICommandHandler<RegisterMemberCommand, IResult<AssociationMember>>
    {
        private readonly IRepository<AssociationMember> _repository;

        public RegisterMemberHandler(IRepository<AssociationMember> repository)
        {
            _repository = repository;
        }

        public async Task<IResult<AssociationMember>> Handle(RegisterMemberCommand request, CancellationToken cancellationToken)
        {
            var member = AssociationMember.Create(request.FirstName, request.LastName).WithEmailAddress(request.Email);

            member = await _repository.AddAsync(member, cancellationToken);

            return Results.Success(member);
        }
    }
}