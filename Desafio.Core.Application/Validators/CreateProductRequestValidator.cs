using Desafio.Core.Application.Contracts.Product.Request;
using FluentValidation;

namespace Desafio.Core.Application.Validators;
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.ProductName)
                .NotEmpty()
                .NotNull()
                .Length(3, 20);
    }
}