﻿using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain.Common;
using HoaM.Domain.Features;

namespace HoaM.Application.Features
{
    public sealed record RegisterResidenceCommand(DevelopmentStatus DevelopmentStatus, params Lot[] Lots) : ICommand<IResult<Residence>> { }

    public sealed class RegisterResidenceValidator : AbstractValidator<RegisterResidenceCommand> 
    {
        public RegisterResidenceValidator(IReadRepository<Parcel> repository)
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

    internal sealed class RegisterResidenceHandler : ICommandHandler<RegisterResidenceCommand, IResult<Residence>>
    {
        private readonly IRepository<Parcel> _repository;

        public RegisterResidenceHandler(IRepository<Parcel> repository)
        {
            _repository = repository;
        }

        public async Task<IResult<Residence>> Handle(RegisterResidenceCommand request, CancellationToken cancellationToken)
        {
            var space = (Residence)await _repository.AddAsync(Residence.Create(request.DevelopmentStatus, request.Lots), cancellationToken);

            return Results.Success(space);
        }
    }
}