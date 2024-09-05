using Desafio.Core.Application.Interfaces.Services;
using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;
using Desafio.Infrastructure.Persistence.Interfaces.Base;

namespace Desafio.Core.Application.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IUnitOfWork _repository;

    public OrderItemService(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<Response<List<OrderItemEntity>>> GetItemByOrderIdAsync(int orderId)
    {
        var response = new Response<List<OrderItemEntity>>();
        _repository.BeginTransaction();
        try
        {
            response.Data = await _repository.OrderItemRepository.GetItemByOrderIdAsync(orderId);

            _repository.CommitTransaction();
            return response;
        }
        catch (Exception ex)
        {
            _repository.RollbackTransaction();
            response.ReportErrors.Add(ReportError.Create($"Transaction not completed. Error message: {ex.Message}"));
            return response;
        }
    }
}