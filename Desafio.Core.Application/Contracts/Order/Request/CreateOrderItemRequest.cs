namespace Desafio.Core.Application.Contracts.Order.Request;

public sealed class CreateOrderItemRequest
{
    public int ProductId { get; set; }
    public int Amount { get; set; }
}