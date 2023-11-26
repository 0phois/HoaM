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
        public RegisterGreenSpaceValidator(IReadRepository<Parcel> repository)
        {
            RuleFor(command => command.DevelopmentStatus).IsInEnum();

            RuleFor(command => command.Lots)
                .NotEmpty()
                .MustAsync(async (lots, cancellationToken) =>
                {
                    var spec = new ParcelByLotsSpec(lots);
                    var parcel = await repository.FirstOrDefaultAsync(spec, cancellationToken);

                    return parcel is null;
                })
                .WithErrorCode(ApplicationErrors.Lot.AlreadyRegistered.Code)
                .WithMessage(ApplicationErrors.Lot.AlreadyRegistered.Message);
        }
    }

    internal sealed class RegisterGreenSpaceHandler(IRepository<Parcel> repository) : ICommandHandler<RegisterGreenSpaceCommand, GreenSpace>
    {
        public async Task<IResult<GreenSpace>> Handle(RegisterGreenSpaceCommand request, CancellationToken cancellationToken)
        {
            var space = (GreenSpace)await repository.AddAsync(GreenSpace.Create(request.DevelopmentStatus, request.Lots), cancellationToken);

            return Results.Success(space);
        }
    }
}
