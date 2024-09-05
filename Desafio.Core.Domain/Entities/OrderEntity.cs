using Desafio.Core.Domain.Common;

namespace Desafio.Core.Domain.Entities;

public class OrderEntity : EntityCommon
{
    public string ClientName { get; set; }
    public string ClientEmail { get; set; }
    public bool IsPaid {  get; set; }
    public int UserId { get; set; }
    public UserEntity User { get; set; }
    public List<OrderItemEntity> OrderItems { get; set; }
}
