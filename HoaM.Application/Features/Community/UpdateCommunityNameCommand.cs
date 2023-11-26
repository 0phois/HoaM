using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record UpdateCommunityNameCommand(CommunityId CommunityId, CommunityName NewName) : ICommand, ICommandBinder<Community, CommunityId>
    {
        public Community? Entity { get; set; }
    }

    public sealed class UpdateCommunityNameValidator : AbstractValidator<UpdateCommunityNameCommand>
    {
        public UpdateCommunityNameValidator(IReadRepository<Community> repository)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.CommunityId).NotEmpty();

            RuleFor(command => command.Entity)
                .NotEmpty()
                    .WithErrorCode(ApplicationErrors.Community.NotFound.Code)
                    .WithMessage(ApplicationErrors.Community.NotFound.Message);

            RuleFor(command => command.NewName)
                .NotEmpty()
                .MustAsync(async (newName, cancellationToken) =>
                {
                    var spec = new CommunityByNameSpec(newName);
                    var community = await repository.FirstOrDefaultAsync(spec, cancellationToken);

                    return community is null;
                }).WithErrorCode(ApplicationErrors.Community.DuplicateName.Code)
                  .WithMessage(ApplicationErrors.Community.DuplicateName.Message)
                  .When(command => !command.Entity!.Name.Value.Equals(command.NewName.Value, StringComparison.OrdinalIgnoreCase));
        }
    }

    public sealed class UpdateCommunityNameCommandHandler : ICommandHandler<UpdateCommunityNameCommand>
    {
        public Task<IResult> Handle(UpdateCommunityNameCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.EditName(request.NewName);

            return Results.Success();
        }
    }
}
