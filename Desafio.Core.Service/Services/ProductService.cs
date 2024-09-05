using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;
using Desafio.Core.Service.Interfaces;
using Desafio.Infrastructure.Persistence.Interfaces.Base;

namespace Desafio.Core.Service.Services;


public class ProductService : IProductService
{
    private readonly IUnitOfWork _repository;

    public ProductService(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<Response> CreateAsync(ProductEntity request)
    {
        var response = new Response<ProductEntity>();
        _repository.BeginTransaction();
        try
        {
            await _repository.ProductRepository.CreateAsync(request);
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

    public async Task<Response> DeleteAsync(int id)
    {
        var response = new Response();
        _repository.BeginTransaction();

        try
        {
            await _repository.ProductRepository.DeleteAsync(id);
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

    public async Task<Response<List<ProductEntity>>> GetAllAsync()
    {
        var response = new Response<List<ProductEntity>>();
        _repository.BeginTransaction();

        try
        {
            var result = await _repository.ProductRepository.GetAllAsync();
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

    public async Task<Response<ProductEntity>> GetByIdAsync(int id)
    {
        var response = new Response<ProductEntity>();
        _repository.BeginTransaction();

        try
        {
            var result = await _repository.ProductRepository.GetByIdAsync(id);
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

    public async Task<Response> UpdateAsync(ProductEntity request)
    {
        var response = new Response();
        _repository.BeginTransaction();

        try
        {
            await _repository.ProductRepository.UpdateAsync(request);
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