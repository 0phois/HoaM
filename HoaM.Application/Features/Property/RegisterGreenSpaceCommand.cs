using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record RegisterGreenSpaceCommand(DevelopmentStatus DevelopmentStatus, params Lot[] Lots) : ICommand<IResult<GreenSpace>> { }

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

    internal sealed class RegisterGreenSpaceHandler : ICommandHandler<RegisterGreenSpaceCommand, IResult<GreenSpace>> 
    {
        private readonly IRepository<Parcel> _repository;

        public RegisterGreenSpaceHandler(IRepository<Parcel> repository)
        {
            _repository = repository;
        }

        public async Task<IResult<GreenSpace>> Handle(RegisterGreenSpaceCommand request, CancellationToken cancellationToken)
        {
            var space = (GreenSpace) await _repository.AddAsync(GreenSpace.Create(request.DevelopmentStatus, request.Lots), cancellationToken);

            return Results.Success(space);
        }
    }
}
