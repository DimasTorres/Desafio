using Desafio.Core.Domain.Entities;

namespace Desafio.Infrastructure.Persistence.Interfaces;

public interface IUserRepository
{
    Task<int> CreateAsync(UserEntity request);
    Task UpdateAsync(UserEntity request);
    Task<UserEntity> GetByLoginAsync(string login);
    Task DeleteAsync(int id);
    Task<List<UserEntity>> GetAllAsync();
    Task<UserEntity> GetByIdAsync(int id);
}