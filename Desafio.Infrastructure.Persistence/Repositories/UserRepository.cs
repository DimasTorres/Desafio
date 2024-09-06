using Dapper;
using Desafio.Core.Domain.Entities;
using Desafio.Infrastructure.Persistence.Data;
using Desafio.Infrastructure.Persistence.Interfaces;
using Desafio.Infrastructure.Persistence.Statements;

namespace Desafio.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnector _dbConnector;
    public UserRepository(IDbConnector dbConnector)
    {
        _dbConnector = dbConnector;
    }

    public async Task<int> CreateAsync(UserEntity request)
    {
        var sql = UserStatements.SQL_INSERT;
        var result = await _dbConnector.dbConnection.ExecuteScalarAsync(sql,
           new
           {
               Name = request.Name,
               Email = request.Email,
               Login = request.Login,
               PasswordHash = request.PasswordHash,
               IsDeleted = false,
               CreatedAt = DateTime.UtcNow
           }, _dbConnector.dbTransaction);

        return Convert.ToInt32(result);
    }

    public async Task DeleteAsync(int id)
    {
        var sql = UserStatements.SQL_DELETE;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = id
            }, _dbConnector.dbTransaction);
    }

    public async Task<List<UserEntity>> GetAllAsync()
    {
        var sql = $"{UserStatements.SQL_BASE}";
        var result = await _dbConnector.dbConnection.QueryAsync<UserEntity>(sql,null, _dbConnector.dbTransaction);

        return result.ToList();
    }

    public async Task<UserEntity> GetByIdAsync(int id)
    {
        var sql = $"{UserStatements.SQL_BASE} AND Id = @Id ";

        var result = await _dbConnector.dbConnection.QueryAsync<UserEntity>(sql,
            new
            {
                Id = id
            }, _dbConnector.dbTransaction);

        return result.FirstOrDefault()!;
    }

    public async Task<UserEntity> GetByLoginAsync(string login)
    {
        var sql = $"{UserStatements.SQL_BASE} AND Login LIKE @Login ";

        var result = await _dbConnector.dbConnection.QueryAsync<UserEntity>(sql,
            new
            {
                Login = login
            }, _dbConnector.dbTransaction);

        return result.FirstOrDefault()!;
    }

    public async Task UpdateAsync(UserEntity request)
    {
        var sql = UserStatements.SQL_UPDATE;

        await _dbConnector.dbConnection.ExecuteAsync(sql,
            new
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                Login = request.Login,
                PasswordHash = request.PasswordHash
            }, _dbConnector.dbTransaction);
    }
}
