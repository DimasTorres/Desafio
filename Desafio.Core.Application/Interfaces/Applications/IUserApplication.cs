using Desafio.Core.Application.Contracts.User.Request;
using Desafio.Core.Application.Contracts.User.Response;
using Desafio.Core.Domain.Common;

namespace Desafio.Core.Application.Interfaces.Applications;

public interface IUserApplication
{
    Task<Response<AuthResponse>> AutheticationAsync(AuthRequest request);
    Task<Response<CreateUserResponse>> CreateAsync(CreateUserRequest request);
    Task<Response> UpdateAsync(UpdateUserRequest request);
    Task<Response> DeleteAsync(int id);
    Task<Response> GetByIdAsync(int id);
    Task<Response<List<UserResponse>>> GetAllAsync();
}