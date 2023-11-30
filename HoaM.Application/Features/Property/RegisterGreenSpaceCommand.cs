using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain.Common;
using HoaM.Domain.Features;
using TanvirArjel.EFCore.GenericRepository;

namespace HoaM.Application.Features
{
    public sealed record RegisterGreenSpaceCommand(DevelopmentStatus DevelopmentStatus, params Lot[] Lots) : ICommand<GreenSpace> { }

    public sealed class RegisterGreenSpaceValidator : AbstractValidator<RegisterGreenSpaceCommand>
    {
        public RegisterGreenSpaceValidator(IRepository repository)
        {
            RuleFor(command => command.DevelopmentStatus).IsInEnum();

            RuleFor(command => command.Lots)
                .NotEmpty()
                .MustAsync(async (lots, cancellationToken) =>
                {
                    var spec = new ParcelByLotsSpec(lots);
                    var parcel = await repository.GetAsync(spec, true, cancellationToken);

                    return parcel is null;
                })
                .WithErrorCode(ApplicationErrors.Lot.AlreadyRegistered.Code)
                .WithMessage(ApplicationErrors.Lot.AlreadyRegistered.Message);
        }
    }

    public sealed class RegisterGreenSpaceHandler(IRepository<Parcel> repository) : ICommandHandler<RegisterGreenSpaceCommand, GreenSpace>
    {
        public async Task<IResult<GreenSpace>> Handle(RegisterGreenSpaceCommand request, CancellationToken cancellationToken)
        {
            var greenSpace = GreenSpace.Create(request.DevelopmentStatus, request.Lots);
            
            await repository.AddAsync<Parcel>(greenSpace, cancellationToken);

            return Results.Success(greenSpace);
        }
    }
}
