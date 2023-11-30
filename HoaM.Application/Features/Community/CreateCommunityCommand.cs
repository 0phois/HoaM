using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;
using TanvirArjel.EFCore.GenericRepository;

namespace HoaM.Application.Features
{
    public sealed record CreateCommunityCommand(CommunityName Name) : ICommand<CommunityId> { }

    public sealed class CreateCommunityValidator : AbstractValidator<CreateCommunityCommand>
    {
        public CreateCommunityValidator(IRepository repository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Name)
                .NotEmpty()
                .MustAsync(async (name, cancellationToken) =>
                {
                    var spec = new CommunityByNameSpec(name);
                    var community = await repository.GetAsync(spec, true, cancellationToken);

                    return community is null;
                })
                .WithErrorCode(ApplicationErrors.Community.DuplicateName.Code)
                .WithMessage(ApplicationErrors.Community.DuplicateName.Message);
        }
    }

    public sealed class CreateCommunityCommandHandler(IRepository<Community> repository) : ICommandHandler<CreateCommunityCommand, CommunityId>
    {
        public async Task<IResult<CommunityId>> Handle(CreateCommunityCommand request, CancellationToken cancellationToken)
        {
            var community = Community.Create(request.Name);
            
            await repository.AddAsync(community, cancellationToken);

            return Results.Success(community.Id);
        }
    }
}
