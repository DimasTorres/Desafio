using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Service.Interfaces
{
    public interface IOrderItemService
    {
        Task<Response<List<OrderItemEntity>>> GetItemByOrderIdAsync(int orderId);
    }
}
