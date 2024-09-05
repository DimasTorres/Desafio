using Desafio.Core.Application.Contracts.User.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Core.Application.Contracts.Order.Response;

public sealed class OrderResponse
{
    public int Id { get; set; }
    public string ClientName { get; set; }
    public string ClientEmail { get; set; }
    public bool IsPaid { get; set; }
    public decimal TotalAmount { get; set; }
    public UserSimpleResponse User { get; set; }
    public List<OrderItemResponse> OrderItems { get; set; }
}