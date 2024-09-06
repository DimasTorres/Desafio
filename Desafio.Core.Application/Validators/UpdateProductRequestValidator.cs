using Desafio.Core.Application.Contracts.Product.Request;
using FluentValidation;

namespace Desafio.Core.Application.Validators;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.ProductName)
                .NotEmpty()
                .Length(3, 20);
    }
}
