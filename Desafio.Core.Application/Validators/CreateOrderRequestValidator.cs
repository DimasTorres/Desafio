using Desafio.Core.Application.Contracts.Order.Request;
using FluentValidation;

namespace Desafio.Core.Application.Validators;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.ClientName)
            .Length(3, 60)
            .NotEmpty();

        RuleFor(x => x.ClientEmail)
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .NotEmpty();
    }
}