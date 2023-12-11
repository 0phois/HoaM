using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record CreateLotCommand(LotNumber LotNumber) : ICommand<Lot> { }

    public sealed class CreateLotValidator : AbstractValidator<CreateLotCommand>
    {
        public CreateLotValidator(ILotRepository repository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.LotNumber)
                .NotEmpty()
                .MustAsync(repository.IsLotNumberUnique)
                .WithErrorCode(ApplicationErrors.Lot.DuplicateNumber.Code)
                .WithMessage(ApplicationErrors.Lot.DuplicateNumber.Message);
        }
    }

    public sealed class CreateLotHandler(ILotRepository repository) : ICommandHandler<CreateLotCommand, Lot>
    {
        public Task<IResult<Lot>> Handle(CreateLotCommand request, CancellationToken cancellationToken)
        {
            var lot = Lot.Create(request.LotNumber);

            repository.Insert(lot);

            return Results.Success(lot);
        }
    }
}
