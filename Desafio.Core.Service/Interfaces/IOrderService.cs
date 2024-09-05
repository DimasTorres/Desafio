using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Service.Interfaces;

public interface IOrderService
{
    Task<Response> CreateAsync(OrderEntity request);
    Task<Response<List<OrderEntity>>> GetAllAsync();
    Task<Response<OrderEntity>> GetByIdAsync(int id);
}