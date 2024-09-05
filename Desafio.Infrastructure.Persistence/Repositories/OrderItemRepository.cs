using Dapper;
using Desafio.Core.Domain.Entities;
using Desafio.Infrastructure.Persistence.Data;
using Desafio.Infrastructure.Persistence.Interfaces;
using Desafio.Infrastructure.Persistence.Statements;

namespace Desafio.Infrastructure.Persistence.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly IDbConnector _dbConnector;

    public OrderItemRepository(IDbConnector dbConnector)
    {
        _dbConnector = dbConnector;
    }

    public async Task CreateItemAsync(OrderItemEntity request)
    {
        var sql = OrderItemStatements.SQL_INSERT;
        
        await _dbConnector.dbConnection.ExecuteScalarAsync(sql,
            new
            {
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                Amount = request.Amount,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            }, _dbConnector.dbTransaction);
    }

    public async Task<List<OrderItemEntity>> GetItemByOrderIdAsync(int orderId)
    {
        var sql = $"{OrderItemStatements.SQL_BASE} AND o.Id = @OrderId ";

        var result = await _dbConnector.dbConnection.QueryAsync<OrderItemEntity, OrderEntity, ProductEntity, OrderItemEntity>(sql,
            map: (orderItem, order, product) =>
            {
                orderItem.Order = order;
                orderItem.Product = product;
                return orderItem;
            },
            param: new
            {
                OrderId = orderId.ToString()
            }, _dbConnector.dbTransaction);

        return result.ToList();
    }
}