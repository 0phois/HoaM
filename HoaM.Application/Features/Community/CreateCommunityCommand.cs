using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record CreateCommunityCommand(CommunityName Name) : ICommand<CommunityId> { }

    public sealed class CreateCommunityValidator : AbstractValidator<CreateCommunityCommand>
    {
        public CreateCommunityValidator(ICommunityRepository repository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Name)
                .NotEmpty()
                .MustAsync(repository.IsNameUniqueAsync)
                .WithErrorCode(ApplicationErrors.Community.DuplicateName.Code)
                .WithMessage(ApplicationErrors.Community.DuplicateName.Message);
        }
    }

    public sealed class CreateCommunityCommandHandler(ICommunityRepository repository) : ICommandHandler<CreateCommunityCommand, CommunityId>
    {
        public Task<IResult<CommunityId>> Handle(CreateCommunityCommand request, CancellationToken cancellationToken)
        {
            var community = Community.Create(request.Name);

            repository.Insert(community);

            return Results.Success(community.Id);
        }
    }
}
