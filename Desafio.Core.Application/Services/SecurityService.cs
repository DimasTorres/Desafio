using Desafio.Core.Application.Interfaces.Services;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Application.Services;

public class SecurityService : ISecurityService
{
    public Task<string> EncryptPassword(string password)
    {
        //encrypt data
        var data = BCrypt.Net.BCrypt.HashPassword(password);

        return Task.FromResult<string>(data);
    }

    public Task<string> DecryptPassword(string passwordHash)
    {
        var data = BCrypt.Net.BCrypt.HashPassword(passwordHash);

        return Task.FromResult<string>(data);
    }

    public Task<bool> VerifyPassword(string password, UserEntity user)
    {
        bool validPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        return Task.FromResult(validPassword);
    }
}