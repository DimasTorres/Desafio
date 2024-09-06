using Desafio.Core.Application.Contracts.Order.Request;
using Desafio.Core.Application.Validators;
using FluentValidation.TestHelper;

namespace Desafio.Test.UnitTest.Validators;

public class CreateOrderRequestValidatorTests
{
    private readonly CreateOrderRequestValidator _validator;

    public CreateOrderRequestValidatorTests()
    {
        _validator = new CreateOrderRequestValidator();
    }

    [Fact]
    public void Should_Have_Error_When_ClientName_Is_Empty()
    {
        var model = new CreateOrderRequest { ClientName = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ClientName);
    }

    [Fact]
    public void Should_Have_Error_When_ClientName_Is_Too_Short()
    {
        var model = new CreateOrderRequest { ClientName = "Jo" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ClientName);
    }

    [Fact]
    public void Should_Not_Have_Error_When_ClientName_Is_Valid()
    {
        var model = new CreateOrderRequest { ClientName = "Valid Name" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.ClientName);
    }

    [Fact]
    public void Should_Have_Error_When_ClientEmail_Is_Empty()
    {
        var model = new CreateOrderRequest { ClientEmail = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ClientEmail);
    }

    [Fact]
    public void Should_Have_Error_When_ClientEmail_Is_Invalid()
    {
        var model = new CreateOrderRequest { ClientEmail = "invalid-email" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ClientEmail);
    }

    [Fact]
    public void Should_Not_Have_Error_When_ClientEmail_Is_Valid()
    {
        var model = new CreateOrderRequest { ClientEmail = "valid@example.com" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.ClientEmail);
    }
}
