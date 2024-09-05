using Desafio.Core.Application.Interfaces.Services;
using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;
using Desafio.Infrastructure.Persistence.Interfaces.Base;

namespace Desafio.Core.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _repository;
    private readonly ISecurityService _securityService;

    public UserService(IUnitOfWork repository, ISecurityService securityService)
    {
        _repository = repository;
        _securityService = securityService;
    }

    public async Task<Response<bool>> AuthenticationAsync(string password, UserEntity user)
    {
        var result = await _securityService.VerifyPassword(password, user);
        return new Response<bool>(result);
    }

    public async Task<Response> CreateAsync(UserEntity request)
    {
        var response = new Response<UserEntity>();
        _repository.BeginTransaction();

        try
        {
            request.PasswordHash = await _securityService.EncryptPassword(request.PasswordHash);

            await _repository.UserRepository.CreateAsync(request);
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
            await _repository.UserRepository.DeleteAsync(id);
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

    public async Task<Response<List<UserEntity>>> GetAllAsync()
    {
        var response = new Response<List<UserEntity>>();
        _repository.BeginTransaction();

        try
        {
            var result = await _repository.UserRepository.GetAllAsync();
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

    public async Task<Response<UserEntity>> GetByIdAsync(int id)
    {
        var response = new Response<UserEntity>();
        _repository.BeginTransaction();

        try
        {
            var result = await _repository.UserRepository.GetByIdAsync(id);
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

    public async Task<Response<UserEntity>> GetByLoginAsync(string login)
    {
        var response = new Response<UserEntity>();
        _repository.BeginTransaction();

        try
        {
            var result = await _repository.UserRepository.GetByLoginAsync(login);
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

    public async Task<Response> UpdateAsync(UserEntity request)
    {
        var response = new Response();
        _repository.BeginTransaction();

        try
        {
            request.PasswordHash = await _securityService.EncryptPassword(request.PasswordHash);
            await _repository.UserRepository.UpdateAsync(request);

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
