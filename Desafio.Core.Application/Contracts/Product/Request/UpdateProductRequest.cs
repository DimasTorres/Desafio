namespace Desafio.Core.Application.Contracts.Product.Request;

public sealed class UpdateProductRequest
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal Value { get; set; }
}