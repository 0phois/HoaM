﻿using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record CreateLotCommand(LotNumber LotNumber) : ICommand<IResult<Lot>> { }

    public sealed class CreateLotValidator : AbstractValidator<CreateLotCommand>
    {
        public CreateLotValidator(IReadRepository<Lot> repository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(command => command.LotNumber)
                .NotEmpty()
                .MustAsync(async (number, cancellationToken) =>
                {
                    var spec = new LotByNumberSpec(number);
                    var lot = await repository.FirstOrDefaultAsync(spec, cancellationToken);

                    return lot is null;
                })
                .WithErrorCode(ApplicationErrors.Lot.DuplicateNumber.Code)
                .WithMessage(ApplicationErrors.Lot.DuplicateNumber.Message);

        }
    }

    internal sealed class CreateLotHandler : ICommandHandler<CreateLotCommand, IResult<Lot>>
    {
        private readonly IRepository<Lot> _repository;

        public CreateLotHandler(IRepository<Lot> repository)
        {
            _repository = repository;
        }

        public async Task<IResult<Lot>> Handle(CreateLotCommand request, CancellationToken cancellationToken)
        {
            var lot = await _repository.AddAsync(Lot.Create(request.LotNumber), cancellationToken);

            return Results.Success(lot);
        }
    }
}