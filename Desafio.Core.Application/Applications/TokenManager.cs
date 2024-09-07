﻿using Desafio.Core.Application.Contracts.User.Response;
using Desafio.Core.Application.Interfaces.Applications;
using Desafio.Core.Application.Models;
using Desafio.Core.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Desafio.Core.Application.Applications;

public class TokenManager : ITokenManager
{
    private readonly AuthSettings _authSettings;

    public TokenManager(IOptions<AuthSettings> authSettings)
    {
        _authSettings = authSettings.Value;
    }

    public Task<AuthResponse> GenerateTokenAsync(UserEntity user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var date = DateTime.UtcNow;
        var expire = date.AddHours(_authSettings.ExpireIn);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Secret));

        var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor()
        {
            Issuer = "desafionet",
            IssuedAt = date,
            NotBefore = date,
            Expires = expire,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(new GenericIdentity(user.Login, JwtBearerDefaults.AuthenticationScheme), new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            })
        });

        var response = new AuthResponse()
        {
            Token = tokenHandler.WriteToken(securityToken),
            ExpireIn = _authSettings.ExpireIn,
            Type = JwtBearerDefaults.AuthenticationScheme
        };

        return Task.FromResult(response);
    }
}
