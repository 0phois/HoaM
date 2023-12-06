using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record RegisterGreenSpaceCommand(DevelopmentStatus DevelopmentStatus, params Lot[] Lots) : ICommand<GreenSpace> { }

    public sealed class RegisterGreenSpaceValidator : AbstractValidator<RegisterGreenSpaceCommand>
    {
        public RegisterGreenSpaceValidator(IParcelRepository repository)
        {
            RuleFor(command => command.DevelopmentStatus).IsInEnum();

            RuleFor(command => command.Lots)
                .NotEmpty()
                .MustAsync(repository.HasUniqueLotsAsync)
                .WithErrorCode(ApplicationErrors.Lot.AlreadyRegistered.Code)
                .WithMessage(ApplicationErrors.Lot.AlreadyRegistered.Message);
        }
    }

    public sealed class RegisterGreenSpaceHandler(IParcelRepository repository) : ICommandHandler<RegisterGreenSpaceCommand, GreenSpace>
    {
        public Task<IResult<GreenSpace>> Handle(RegisterGreenSpaceCommand request, CancellationToken cancellationToken)
        {
            var greenSpace = GreenSpace.Create(request.DevelopmentStatus, request.Lots);

            repository.Insert(greenSpace);

            return Results.Success(greenSpace);
        }
    }
}
