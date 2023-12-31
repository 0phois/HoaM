﻿using FluentValidation;
using HoaM.Application.Common;
using HoaM.Application.Exceptions;
using HoaM.Domain;
using HoaM.Domain.Common;

namespace HoaM.Application
{
    public sealed record RegisterGreenSpaceCommand(DevelopmentStatus DevelopmentStatus, params Lot[] Lots) : ICommand<IResult<GreenSpace>> { }

    public sealed class RegisterGreenSpaceValidator : AbstractValidator<RegisterGreenSpaceCommand>
    {
        public RegisterGreenSpaceValidator(IParcelRepository repository)
        {
            RuleFor(command => command.DevelopmentStatus).IsInEnum();

            RuleFor(command => command.Lots)
                .NotEmpty()
                .MustAsync(repository.HasUniqueLotsAsync)
                .WithErrorCode(ApplicationErrors.Lot.AlreadyRegistered.Code)
                .WithMessage(ApplicationErrors.Lot.AlreadyRegistered.Message);
        }
    }

    public sealed class RegisterGreenSpaceHandler(IParcelRepository repository) : ICommandHandler<RegisterGreenSpaceCommand, IResult<GreenSpace>>
    {
        public Task<IResult<GreenSpace>> Handle(RegisterGreenSpaceCommand request, CancellationToken cancellationToken)
        {
            var greenSpace = GreenSpace.Create(request.DevelopmentStatus, request.Lots);

            repository.Insert(greenSpace);

            return Results.Success(greenSpace);
        }
    }
}
