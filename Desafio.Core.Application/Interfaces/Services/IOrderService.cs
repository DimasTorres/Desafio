using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Application.Interfaces.Services;

public interface IOrderService
{
    Task<Response> CreateAsync(OrderEntity request);
    Task<Response<List<OrderEntity>>> GetAllAsync();
    Task<Response<OrderEntity>> GetByIdAsync(int id);
}