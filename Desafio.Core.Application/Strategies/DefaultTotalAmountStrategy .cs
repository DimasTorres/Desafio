using Desafio.Core.Application.Contracts.Order.Response;
using Desafio.Core.Application.Interfaces.Strategies;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Application.Strategies;

public class DefaultCalculateStrategy : ICalculateStrategy
{
    public decimal CalculateTotalAmount(List<OrderItemEntity> orderItems)
    {
        decimal totalAmount = 0;
        foreach (var item in orderItems)
        {
            totalAmount += (item.Amount * item.Product.Value);
        }

        return totalAmount;
    }

    public decimal CalculateTotalAmount(List<OrderItemResponse> orderItems)
    {
        decimal totalAmount = 0;
        foreach (var item in orderItems)
        {
            totalAmount += (item.Amount * item.Product.Value);
        }

        return totalAmount;
    }
}