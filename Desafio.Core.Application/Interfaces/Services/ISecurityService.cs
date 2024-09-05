using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Application.Interfaces.Services;

public interface ISecurityService
{
    Task<string> EncryptPassword(string password);
    Task<string> DecryptPassword(string passwordHash);
    Task<bool> VerifyPassword(string password, UserEntity user);
}
