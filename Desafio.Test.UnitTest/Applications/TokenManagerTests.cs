using Desafio.Core.Application.Applications;
using Desafio.Core.Application.Models;
using Desafio.Core.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSubstitute;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Desafio.Test.UnitTest.Applications;

public class TokenManagerTests
{
    [Fact]
    public async Task GenerateTokenAsync_Should_Return_Valid_Token()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Login = "testuser" };
        var authSettings = new AuthSettings
        {
            Secret = "super_secret_key_that_should_be_long_enough",
            ExpireIn = 1
        };

        var options = Substitute.For<IOptions<AuthSettings>>();
        options.Value.Returns(authSettings);

        var tokenManager = new TokenManager(options);

        // Act
        var response = await tokenManager.GenerateTokenAsync(user);

        // Assert
        Assert.NotEmpty(response.Token);
        Assert.Equal(response.ExpireIn, authSettings.ExpireIn);

        // Validate token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(authSettings.Secret);
        tokenHandler.ValidateToken(response.Token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        Assert.NotNull(validatedToken);

        var jwtToken = validatedToken as JwtSecurityToken;
        Assert.NotNull(jwtToken);        
    }
}
