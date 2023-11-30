using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;
using TanvirArjel.EFCore.GenericRepository;

namespace HoaM.Application.Features
{
    public sealed record CreateLotCommand(LotNumber LotNumber) : ICommand<Lot> { }

    public sealed class CreateLotValidator : AbstractValidator<CreateLotCommand>
    {
        public CreateLotValidator(IRepository repository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.LotNumber)
                .NotEmpty()
                .MustAsync(async (number, cancellationToken) =>
                {
                    var spec = new LotByNumberSpec(number);
                    var lot = await repository.GetAsync(spec, true, cancellationToken);

                    return lot is null;
                })
                .WithErrorCode(ApplicationErrors.Lot.DuplicateNumber.Code)
                .WithMessage(ApplicationErrors.Lot.DuplicateNumber.Message);

        }
    }

    public sealed class CreateLotHandler(IRepository<Lot> repository) : ICommandHandler<CreateLotCommand, Lot>
    {
        public async Task<IResult<Lot>> Handle(CreateLotCommand request, CancellationToken cancellationToken)
        {
            var lot = Lot.Create(request.LotNumber);
            
            await repository.AddAsync(lot, cancellationToken);

            return Results.Success(lot);
        }
    }
}
