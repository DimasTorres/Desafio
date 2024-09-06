using Desafio.Core.Domain.Entities;

namespace Desafio.Infrastructure.Persistence.Interfaces;

public interface IProductRepository
{
    Task<int> CreateAsync(ProductEntity request);
    Task UpdateAsync(ProductEntity request);
    Task DeleteAsync(int id);
    Task<List<ProductEntity>> GetAllAsync();
    Task<ProductEntity> GetByIdAsync(int id);
}