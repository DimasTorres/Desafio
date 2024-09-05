using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Application.Interfaces.Services;

public interface IOrderItemService
{
    Task<Response<List<OrderItemEntity>>> GetItemByOrderIdAsync(int orderId);
}
