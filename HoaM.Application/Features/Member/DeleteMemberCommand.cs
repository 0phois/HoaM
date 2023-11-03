using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record DeleteMemberCommand(AssociationMember Member) : ICommand<IResult> { }

    public sealed class DeleteMemberValidator :AbstractValidator<DeleteMemberCommand> 
    {
        public DeleteMemberValidator(IReadRepository<AssociationMember> repository) 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Member)
                .NotEmpty()
                .MustAsync(async (request, cancellationToken) =>
                {
                    var member = await repository.GetByIdAsync(request.Id, cancellationToken);

                    return member is not null && !member.IsDeleted;
                })
                .WithErrorCode(ApplicationErrors.AssociationMember.NotFound.Code)
                .WithMessage(ApplicationErrors.AssociationMember.NotFound.Message); ;
        }
    }

    internal sealed class DeleteMemberHandler : ICommandHandler<DeleteMemberCommand, IResult>
    {
        private readonly IRepository<AssociationMember> _repository;

        public DeleteMemberHandler(IRepository<AssociationMember> repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Member, cancellationToken);

            return Results.Success();
        }
    }
}
