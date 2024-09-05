using Dapper;
using Desafio.Core.Domain.Entities;
using Desafio.Infrastructure.Persistence.Data;
using Desafio.Infrastructure.Persistence.Interfaces;
using Desafio.Infrastructure.Persistence.Statements;

namespace Desafio.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IDbConnector _dbConnector;
    public ProductRepository(IDbConnector dbConnector)
    {
        _dbConnector = dbConnector;
    }

    public async Task CreateAsync(ProductEntity request)
    {
        var sql = ProductStatements.SQL_INSERT;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                ProductName = request.ProductName,
                Value = request.Value,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            }, _dbConnector.dbTransaction);
    }
    public async Task UpdateAsync(ProductEntity request)
    {
        var sql = ProductStatements.SQL_UPDATE;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = request.Id,
                ProductName = request.ProductName,
                Value = request.Value,
            }, _dbConnector.dbTransaction);
    }

    public async Task DeleteAsync(int id)
    {
        var sql = ProductStatements.SQL_DELETE;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = id
            }, _dbConnector.dbTransaction);
    }

    public async Task<List<ProductEntity>> GetAllAsync()
    {
        var sql = $"{ProductStatements.SQL_BASE}";

        var result = await _dbConnector.dbConnection.QueryAsync<ProductEntity>(sql,
            _dbConnector.dbTransaction);

        return result.ToList();
    }

    public async Task<ProductEntity> GetByIdAsync(int id)
    {
        var sql = $"{ProductStatements.SQL_BASE} AND Id = @Id ";

        var result = await _dbConnector.dbConnection.QueryAsync<ProductEntity>(sql,
            new
            {
                Id = id
            }, _dbConnector.dbTransaction);

        return result.FirstOrDefault()!;
    }
}