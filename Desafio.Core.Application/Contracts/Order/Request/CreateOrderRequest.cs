namespace Desafio.Core.Application.Contracts.Order.Request;

public sealed class CreateOrderRequest
{
    public string ClientName { get; set; }
    public string ClientEmail { get; set; }
    public bool IsPaid { get; set; }
    public int UserId { get; set; }
    public List<CreateOrderItemRequest> OrderItems { get; set; }
}