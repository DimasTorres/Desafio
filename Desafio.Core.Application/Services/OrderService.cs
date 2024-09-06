using Desafio.Core.Application.Interfaces.Services;
using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;
using Desafio.Infrastructure.Persistence.Interfaces.Base;

namespace Desafio.Core.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _repository;

    public OrderService(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<Response<int>> CreateAsync(OrderEntity request)
    {
        var response = new Response<int>();
        _repository.BeginTransaction();

        try
        {
            int idResult = await _repository.OrderRepository.CreateAsync(request);

            _repository.CommitTransaction();
            response.Data = idResult;

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

            foreach (var item in result) 
            {
                item.OrderItems = await _repository.OrderItemRepository.GetItemByOrderIdAsync(item.Id);
            }                        
            
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
