using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;
using Desafio.Core.Service.Interfaces;
using Desafio.Infrastructure.Persistence.Interfaces.Base;

namespace Desafio.Core.Service.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _repository;

    public OrderService(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<Response> CreateAsync(OrderEntity request)
    {
        var response = new Response();
        _repository.BeginTransaction();

        try
        {
            await _repository.OrderRepository.CreateAsync(request);

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

    public async Task<Response<List<OrderEntity>>> GetAllAsync()
    {
        var response = new Response<List<OrderEntity>>();
        _repository.BeginTransaction();

        try
        {
            var result = await _repository.OrderRepository.GetAllAsync();
            response.Data = result;

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

    public async Task<Response<OrderEntity>> GetByIdAsync(int id)
    {
        var response = new Response<OrderEntity>();
        _repository.BeginTransaction();

        try
        {
            var result = await _repository.OrderRepository.GetByIdAsync(id);
            result.OrderItems = await _repository.OrderItemRepository.GetItemByOrderIdAsync(id);

            response.Data = result;

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
