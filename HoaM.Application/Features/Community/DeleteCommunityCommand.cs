using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record DeleteCommunityCommand(Community Community) : ICommand<IResult> { }

    public sealed class DeleteCommunityValidator : AbstractValidator<DeleteCommunityCommand> 
    {
        public DeleteCommunityValidator(IReadRepository<Community> repository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Community)
                .NotEmpty()
                .MustAsync(async (request, cancellationToken) =>
                {
                    var community = await repository.GetByIdAsync(request.Id, cancellationToken);

                    return community is not null;
                })
                .WithErrorCode(ApplicationErrors.Community.NotFound.Code)
                .WithMessage(ApplicationErrors.Community.NotFound.Message);
        }
    }

    internal sealed class DeleteCommunityHandler : ICommandHandler<DeleteCommunityCommand, IResult>
    {
        private readonly IRepository<Community> _repository;

        public DeleteCommunityHandler(IRepository<Community> repository)
        {
            _repository = repository;
        }

        public async Task<IResult> Handle(DeleteCommunityCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Community, cancellationToken);

            return Results.Success();
        }
    }
}
