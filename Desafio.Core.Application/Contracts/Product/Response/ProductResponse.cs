namespace Desafio.Core.Application.Contracts.Product.Response;

public sealed class ProductResponse
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Value { get; set; }
}