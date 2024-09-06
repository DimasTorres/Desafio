using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Application.Interfaces.Services;

public interface IUserService
{
    Task<Response<bool>> AuthenticationAsync(string password, string passwordHash);
    Task<Response<int>> CreateAsync(UserEntity request);
    Task<Response> UpdateAsync(UserEntity request);
    Task<Response<UserEntity>> GetByLoginAsync(string login);
    Task<Response> DeleteAsync(int id);
    Task<Response<List<UserEntity>>> GetAllAsync();
    Task<Response<UserEntity>> GetByIdAsync(int id);
}
