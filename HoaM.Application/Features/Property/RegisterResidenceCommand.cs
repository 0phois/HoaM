using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain.Common;
using HoaM.Domain.Features;
using TanvirArjel.EFCore.GenericRepository;

namespace HoaM.Application.Features
{
    public sealed record RegisterResidenceCommand(DevelopmentStatus DevelopmentStatus, params Lot[] Lots) : ICommand<Residence> { }

    public sealed class RegisterResidenceValidator : AbstractValidator<RegisterResidenceCommand>
    {
        public RegisterResidenceValidator(IRepository repository)
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

    public sealed class RegisterResidenceHandler(IRepository repository) : ICommandHandler<RegisterResidenceCommand, Residence>
    {
        public async Task<IResult<Residence>> Handle(RegisterResidenceCommand request, CancellationToken cancellationToken)
        {
            var residence = Residence.Create(request.DevelopmentStatus, request.Lots);
            
            await repository.AddAsync<Parcel>(residence, cancellationToken);

            return Results.Success(residence);
        }
    }
}
