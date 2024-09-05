using Desafio.Core.Application.Contracts.Product.Response;

namespace Desafio.Core.Application.Contracts.Order.Response;

public sealed class OrderItemResponse
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public ProductResponse Product { get; set; }
    public int Amount { get; set; }
    public decimal UnitValue { get; set; }
    public decimal SubTotalAmout { get; set; }
}