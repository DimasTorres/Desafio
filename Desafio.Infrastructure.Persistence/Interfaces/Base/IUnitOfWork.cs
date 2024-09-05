using Desafio.Infrastructure.Persistence.Data;

namespace Desafio.Infrastructure.Persistence.Interfaces.Base;

public interface IUnitOfWork
{
    IOrderRepository OrderRepository { get; }
    IOrderItemRepository OrderItemRepository { get; }
    IProductRepository ProductRepository { get; }
    IUserRepository UserRepository { get; }
    IDbConnector DbConnector { get; }
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}