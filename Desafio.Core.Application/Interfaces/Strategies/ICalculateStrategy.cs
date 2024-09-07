using Desafio.Core.Application.Contracts.Order.Response;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Application.Interfaces.Strategies;

public interface ICalculateStrategy
{
    decimal CalculateTotalAmount(List<OrderItemEntity> orderItems);
    decimal CalculateTotalAmount(List<OrderItemResponse> orderItems);
}
