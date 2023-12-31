﻿using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record RegisterResidenceCommand(DevelopmentStatus DevelopmentStatus, params Lot[] Lots) : ICommand<IResult<Residence>> { }

    public sealed class RegisterResidenceValidator : AbstractValidator<RegisterResidenceCommand>
    {
        public RegisterResidenceValidator(IParcelRepository repository)
        {
            RuleFor(command => command.DevelopmentStatus).IsInEnum();

            RuleFor(command => command.Lots)
                .NotEmpty()
                .MustAsync(repository.HasUniqueLotsAsync)
                .WithErrorCode(ApplicationErrors.Lot.AlreadyRegistered.Code)
                .WithMessage(ApplicationErrors.Lot.AlreadyRegistered.Message);
        }
    }

    public sealed class RegisterResidenceHandler(IParcelRepository repository) : ICommandHandler<RegisterResidenceCommand, IResult<Residence>>
    {
        public Task<IResult<Residence>> Handle(RegisterResidenceCommand request, CancellationToken cancellationToken)
        {
            var residence = Residence.Create(request.DevelopmentStatus, request.Lots);

            repository.Insert(residence);

            return Results.Success(residence);
        }
    }
}
