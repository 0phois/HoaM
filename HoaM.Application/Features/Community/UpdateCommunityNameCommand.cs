using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record UpdateCommunityNameCommand(Community Community, CommunityName NewName) : ICommand<IResult> { }

    public sealed class UpdateCommunityNameValidator : AbstractValidator<UpdateCommunityNameCommand>
    {
        public UpdateCommunityNameValidator(IReadRepository<Community> repository)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.Community).NotEmpty();

            RuleFor(command => command.NewName)
                .NotEmpty()
                .MustAsync(async (newName, cancellationToken) =>
                {
                    var spec = new CommunityByNameSpec(newName);
                    var community = await repository.FirstOrDefaultAsync(spec, cancellationToken);

                    return community is null;
                })
                .When(command => command.Community.Name != command.NewName)
                .WithErrorCode(ApplicationErrors.Community.DuplicateName.Code)
                .WithMessage(ApplicationErrors.Community.DuplicateName.Message);
        }
    }

    internal sealed class UpdateCommunityNameCommandHandler : ICommandHandler<UpdateCommunityNameCommand, IResult>
    {
        public Task<IResult> Handle(UpdateCommunityNameCommand request, CancellationToken cancellationToken)
        {
            request.Community.EditName(request.NewName);

            return Results.Success();
        }
    }
}
