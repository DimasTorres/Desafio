using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Service.Interfaces;

public interface IUserService
{
    Task<Response<bool>> AuthenticationAsync(string password, UserEntity user);
    Task<Response> CreateAsync(UserEntity request);
    Task<Response> UpdateAsync(UserEntity request);
    Task<Response<UserEntity>> GetByLoginAsync(string login);
    Task<Response> DeleteAsync(int id);
    Task<Response<List<UserEntity>>> GetAllAsync();
    Task<Response<UserEntity>> GetByIdAsync(int id);
}
