using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record UpdateParcelAddressCommand(ParcelId ParcelId, StreetName StreetName, StreetNumber StreetNumber) : ICommand<IResult>, IParcelBinder
    {
        public ParcelId Id => ParcelId;
        public Parcel? Entity { get; set; }
    }

    public sealed class UpdateParcelAddressValidator : AbstractValidator<UpdateParcelAddressCommand>
    {
        public UpdateParcelAddressValidator(IParcelRepository repository)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.ParcelId).NotEmpty();

            RuleFor(command => command.StreetName).NotEmpty();

            RuleFor(command => command.StreetNumber).NotEmpty();

            RuleFor(command => command.Entity)
                .NotEmpty()
                .MustAsync((cmd, _, cancellationToken) => repository.IsAddressUniqueAsync(cmd.StreetNumber, cmd.StreetName, cancellationToken))
                .WithErrorCode(ApplicationErrors.Parcel.DuplicateAddress.Code)
                .WithMessage(ApplicationErrors.Parcel.DuplicateAddress.Message);
        }
    }

    public sealed class UpdateParcelAddressHandler : ICommandHandler<UpdateParcelAddressCommand, IResult>
    {
        public Task<IResult> Handle(UpdateParcelAddressCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.WithAddress<Parcel>(request.StreetNumber, request.StreetName);

            return Results.Success();
        }
    }
}
