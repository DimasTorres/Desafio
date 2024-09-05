using Desafio.Core.Application.Contracts.Order.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Application.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.UserId)
               .NotNull();

        RuleFor(x => x.ClientName)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.ClientEmail)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .NotEmpty()
            .NotNull();
    }
}