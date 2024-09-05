using Desafio.Core.Application.Contracts.Product.Request;
using Desafio.Core.Application.Contracts.Product.Response;
using Desafio.Core.Domain.Common;

namespace Desafio.Core.Application.Interfaces.Applications;

public interface IProductApplication
{
    Task<Response> CreateAsync(CreateProductRequest request);
    Task<Response> UpdateAsync(UpdateProductRequest request);
    Task<Response> DeleteAsync(int id);
    Task<Response> GetByIdAsync(int id);
    Task<Response<List<ProductResponse>>> GetAllAsync();
}