using Dapper;
using Desafio.Core.Domain.Entities;
using Desafio.Infrastructure.Persistence.Data;
using Desafio.Infrastructure.Persistence.Interfaces;
using Desafio.Infrastructure.Persistence.Statements;

namespace Desafio.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnector _dbConnector;
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderRepository(IDbConnector dbConnector, IOrderItemRepository orderItemRepository)
    {
        _dbConnector = dbConnector;
        _orderItemRepository = orderItemRepository;
    }

    public async Task<int> CreateAsync(OrderEntity request)
    {
        var sql = OrderStatements.SQL_INSERT;

        var result = await _dbConnector.dbConnection.ExecuteScalarAsync(sql,
            new
            {
                ClientName = request.ClientName,
                ClientEmail = request.ClientEmail,
                IsPaid = request.IsPaid,
                UserId = request.UserId,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            }, _dbConnector.dbTransaction);

        if (request.OrderItems.Any() && result is not null)
        {
            foreach (var item in request.OrderItems)
            {
                item.OrderId = Convert.ToInt32(result);
                await _orderItemRepository.CreateItemAsync(item);
            }
        }

        return Convert.ToInt32(result);
    }

    public async Task<List<OrderEntity>> GetAllAsync()
    {
        var sql = $"{OrderStatements.SQL_BASE}";
        var result = await _dbConnector.dbConnection.QueryAsync<OrderEntity, UserEntity, OrderEntity>(sql,
            map: (order, user) =>
            {
                order.User = user;
                return order;
            }, null, _dbConnector.dbTransaction);

        return result.ToList();
    }

    public async Task<OrderEntity> GetByIdAsync(int id)
    {
        var sql = $"{OrderStatements.SQL_BASE} AND o.Id = @Id ";

        var result = await _dbConnector.dbConnection.QueryAsync<OrderEntity, UserEntity, OrderEntity>(sql,
            map: (order, user) =>
            {
                order.User = user;
                return order;
            },
            param: new
            {
                Id = id
            }, _dbConnector.dbTransaction);

        return result.FirstOrDefault()!;
    }
}
