using Desafio.Core.Application.Contracts.User.Response;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Application.Interfaces.Applications;

public interface ITokenManager
{
    Task<AuthResponse> GenerateTokenAsync(UserEntity user);
}