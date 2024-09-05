using Desafio.Core.Application.Contracts.Order.Request;
using Desafio.Core.Application.Contracts.Order.Response;
using Desafio.Core.Domain.Common;

namespace Desafio.Core.Application.Interfaces.Applications;

public interface IOrderApplication
{
    Task<Response> CreateAsync(CreateOrderRequest request);
    Task<Response> GetByIdAsync(int id);
    Task<Response<List<OrderResponse>>> GetAllAsync();
}