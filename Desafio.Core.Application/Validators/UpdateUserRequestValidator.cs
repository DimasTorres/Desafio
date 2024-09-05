﻿using Desafio.Core.Application.Contracts.User.Request;
using FluentValidation;

namespace Desafio.Core.Application.Validators;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .NotNull()
           .Length(3, 150);

        RuleFor(x => x.Login)
            .NotEmpty()
            .NotNull()
            .Length(3, 20);

        RuleFor(x => x.Password)
         .NotEmpty().WithMessage("Password is required.")
         .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
         .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
         .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
         .Matches("[0-9]").WithMessage("Password must contain at least one number.")
         .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password);

        RuleFor(x => x.Email)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .NotEmpty()
            .NotNull();
    }
}