using FluentValidation;
using HoaM.Application.Common;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record UpdateDevelopmentStatusCommand(ParcelId ParcelId, DevelopmentStatus Status) : ICommand, IParcelBinder
    {
        public ParcelId Id => ParcelId;
        public Parcel? Entity { get; set; }
    }

    public sealed class UpdateDevelopmentStatusValidator : AbstractValidator<UpdateDevelopmentStatusCommand>
    {
        public UpdateDevelopmentStatusValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.ParcelId).NotEmpty();

            RuleFor(command => command.Status).IsInEnum();

            RuleFor(command => command.Entity).NotEmpty();
        }
    }

    public sealed class UpdateDevelopmentStatusHandler : ICommandHandler<UpdateDevelopmentStatusCommand>
    {
        public Task<IResult> Handle(UpdateDevelopmentStatusCommand request, CancellationToken cancellationToken)
        {
            request.Entity!.UpdateDevelopmentStatus(request.Status);

            return Results.Success();
        }
    }
}
