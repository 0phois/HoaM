using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record UpdateCommunityNameCommand(CommunityId CommunityId, CommunityName NewName) : ICommand<IResult>, ICommunityBinder
    {
        public CommunityId Id => CommunityId;
        public Community? Entity { get; set; }
    }

    public sealed class UpdateCommunityNameValidator : AbstractValidator<UpdateCommunityNameCommand>
    {
        public UpdateCommunityNameValidator(ICommunityRepository repository)
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
                .MustAsync(repository.IsNameUniqueAsync)
                    .WithErrorCode(ApplicationErrors.Community.DuplicateName.Code)
                    .WithMessage(ApplicationErrors.Community.DuplicateName.Message)
                    .When(command => !command.Entity!.Name.Value.Equals(command.NewName.Value, StringComparison.OrdinalIgnoreCase));
        }
    }

    public sealed class UpdateCommunityNameCommandHandler : ICommandHandler<UpdateCommunityNameCommand, IResult>
    {
        public Task<IResult> Handle(UpdateCommunityNameCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.EditName(request.NewName);

            return Results.Success();
        }
    }
}
