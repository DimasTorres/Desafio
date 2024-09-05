using Desafio.Core.Domain.Common;

namespace Desafio.Core.Domain.Entities;

public class ProductEntity : EntityCommon
{
    public string ProductName { get; set; }
    public decimal Value { get; set; }
}
