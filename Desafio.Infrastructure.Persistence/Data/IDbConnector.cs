using System.Data;

namespace Desafio.Infrastructure.Persistence.Data;

public interface IDbConnector : IDisposable
{
    IDbConnection dbConnection { get; }
    IDbTransaction dbTransaction { get; set; }
    IDbTransaction BeginTransaction(IsolationLevel isolation);
}
