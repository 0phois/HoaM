using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record DeleteCommunityCommand(CommunityId CommunityId) : ICommand, ICommandBinder<Community, CommunityId>
    {
        public Community? Entity { get; set; }
    }

    public sealed class DeleteCommunityValidator : AbstractValidator<DeleteCommunityCommand>
    {
        public DeleteCommunityValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.CommunityId).NotEmpty();

            RuleFor(command => command.Entity)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.Community.NotFound.Code)
                    .WithMessage(ApplicationErrors.Community.NotFound.Message);
        }
    }

    internal sealed class DeleteCommunityHandler(IRepository<Community> repository) : ICommandHandler<DeleteCommunityCommand>
    {
        public async Task<IResult> Handle(DeleteCommunityCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteAsync(request.Entity!, cancellationToken);

            return Results.Success();
        }
    }
}
