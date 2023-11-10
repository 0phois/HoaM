using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record CreateCommunityCommand(CommunityName Name) : ICommand<IResult<Community>> { }

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

    internal sealed class CreateCommunityCommandHandler : ICommandHandler<CreateCommunityCommand, IResult<Community>>
    {
        private readonly IRepository<Community> _repository;

        public CreateCommunityCommandHandler(IRepository<Community> repository)
        {
            _repository = repository;
        }

        public async Task<IResult<Community>> Handle(CreateCommunityCommand request, CancellationToken cancellationToken)
        {
            var community = await _repository.AddAsync(Community.Create(request.Name), cancellationToken);

            return Results.Success(community);
        }
    }
}
