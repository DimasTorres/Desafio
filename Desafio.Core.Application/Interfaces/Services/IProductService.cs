using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Application.Interfaces.Services;

public interface IProductService
{
    Task<Response> CreateAsync(ProductEntity request);
    Task<Response> UpdateAsync(ProductEntity request);
    Task<Response> DeleteAsync(int id);
    Task<Response<List<ProductEntity>>> GetAllAsync();
    Task<Response<ProductEntity>> GetByIdAsync(int id);
}
