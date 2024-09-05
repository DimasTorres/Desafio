namespace Desafio.Core.Application.Contracts.Product.Request;

public sealed class CreateProductRequest
{
    public string ProductName { get; set; }
    public decimal Value { get; set; }
}