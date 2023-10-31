using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record CreateCommunityCommand(CommunityName Name) : ICommand<IResult> { }

    public sealed class CreateCommunityValidator : AbstractValidator<CreateCommunityCommand>
    {
        public CreateCommunityValidator(IReadRepository<Community> repository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Name)
                .NotEmpty()
                .MustAsync(async (name, cancellationToken) =>
                {
                    var spec = new CommunityByNameSpec(name);
                    var community = await repository.FirstOrDefaultAsync(spec, cancellationToken);
                
                    return community is null;
                })
                .WithErrorCode(ApplicationErrors.Community.DuplicateName.Code)
                .WithMessage(ApplicationErrors.Community.DuplicateName.Message);
        }
    }

    internal sealed class CreateCommunityCommandHandler : ICommandHandler<CreateCommunityCommand, IResult>
    {
        private readonly IRepository<Community> _communityRepository;

        public CreateCommunityCommandHandler(IRepository<Community> communityRepository)
        {
            _communityRepository = communityRepository;
        }

        public async Task<IResult> Handle(CreateCommunityCommand request, CancellationToken cancellationToken)
        {
            await _communityRepository.AddAsync(Community.Create(request.Name), cancellationToken);

            return Results.Success();
        }
    }
}
