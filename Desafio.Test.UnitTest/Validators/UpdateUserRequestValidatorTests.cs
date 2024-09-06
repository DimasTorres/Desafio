using Desafio.Core.Application.Contracts.User.Request;
using Desafio.Core.Application.Validators;
using FluentValidation.TestHelper;

namespace Desafio.Test.UnitTest.Validators;

public class UpdateUserRequestValidatorTests
{
    private readonly UpdateUserRequestValidator _validator;

    public UpdateUserRequestValidatorTests()
    {
        _validator = new UpdateUserRequestValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var model = new UpdateUserRequest { Name = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Name_Is_Valid()
    {
        var model = new UpdateUserRequest { Name = "Valid Name" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Login_Is_Invalid()
    {
        var model = new UpdateUserRequest { Login = "ab" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Login);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Invalid()
    {
        var model = new UpdateUserRequest { Password = "password" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Should_Have_Error_When_ConfirmPassword_Does_Not_Match()
    {
        var model = new UpdateUserRequest { Password = "Password1!", ConfirmPassword = "Password2!" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var model = new UpdateUserRequest { Email = "invalid-email" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Email_Is_Valid()
    {
        var model = new UpdateUserRequest { Email = "valid@example.com" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }
}