using Desafio.Core.Application.Contracts.User.Request;
using Desafio.Core.Application.Validators;
using FluentValidation.TestHelper;

namespace Desafio.Test.UnitTest.Validators;

public class AuthRequestValidatorTests
{
    private readonly AuthRequestValidator _validator;

    public AuthRequestValidatorTests()
    {
        _validator = new AuthRequestValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Login_Is_Empty()
    {
        var model = new AuthRequest { Login = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Login);
    }

    [Fact]
    public void Should_Have_Error_When_Login_Is_Too_Short()
    {
        var model = new AuthRequest { Login = "ab" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Login);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Login_Is_Valid()
    {
        var model = new AuthRequest { Login = "ValidLogin" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Login);
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Empty()
    {
        var model = new AuthRequest { Password = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password)
              .WithErrorMessage("Password is required.");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Is_Too_Short()
    {
        var model = new AuthRequest { Password = "Pass1!" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password)
              .WithErrorMessage("Password must be at least 8 characters long.");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Missing_Uppercase()
    {
        var model = new AuthRequest { Password = "password1!" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password)
              .WithErrorMessage("Password must contain at least one uppercase letter.");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Missing_Lowercase()
    {
        var model = new AuthRequest { Password = "PASSWORD1!" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password)
              .WithErrorMessage("Password must contain at least one lowercase letter.");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Missing_Number()
    {
        var model = new AuthRequest { Password = "Password!" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password)
              .WithErrorMessage("Password must contain at least one number.");
    }

    [Fact]
    public void Should_Have_Error_When_Password_Missing_Special_Character()
    {
        var model = new AuthRequest { Password = "Password1" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Password)
              .WithErrorMessage("Password must contain at least one special character.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Password_Is_Valid()
    {
        var model = new AuthRequest { Password = "Password1!" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }
}
