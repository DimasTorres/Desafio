using Desafio.Core.Domain.Entities;

namespace Desafio.Infrastructure.Persistence.Interfaces;

public interface IOrderItemRepository
{
    Task CreateItemAsync(OrderItemEntity request);    
    Task<List<OrderItemEntity>> GetItemByOrderIdAsync(int orderId);
}