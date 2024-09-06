using Desafio.Core.Application.Contracts.Product.Request;
using Desafio.Core.Application.Validators;
using FluentValidation.TestHelper;

namespace Desafio.Test.UnitTest.Validators;

public class CreateProductRequestValidatorTests
{
    private readonly CreateProductRequestValidator _validator;

    public CreateProductRequestValidatorTests()
    {
        _validator = new CreateProductRequestValidator();
    }

    [Fact]
    public void Should_Have_Error_When_ProductName_Is_Empty()
    {
        var model = new CreateProductRequest { ProductName = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ProductName);
    }

    [Fact]
    public void Should_Have_Error_When_ProductName_Is_Too_Short()
    {
        var model = new CreateProductRequest { ProductName = "ab" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ProductName);
    }

    [Fact]
    public void Should_Have_Error_When_ProductName_Is_Too_Long()
    {
        var model = new CreateProductRequest { ProductName = "ThisProductNameIsWayTooLong" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ProductName);
    }

    [Fact]
    public void Should_Not_Have_Error_When_ProductName_Is_Valid()
    {
        var model = new CreateProductRequest { ProductName = "ValidName" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.ProductName);
    }
}