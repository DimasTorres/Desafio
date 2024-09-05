using Desafio.Core.Domain.Common;

namespace Desafio.Core.Domain.Entities;

public class OrderItemEntity : EntityCommon
{
    public int OrderId { get; set; }
    public OrderEntity Order { get; set; }
    public int ProductId { get; set; }
    public ProductEntity Product { get; set; }
    public int Amount { get; set; }
}
