using AutoMapper;
using Desafio.Core.Application.Contracts.User.Request;
using Desafio.Core.Application.Contracts.User.Response;
using Desafio.Core.Application.Interfaces.Applications;
using Desafio.Core.Application.Interfaces.Services;
using Desafio.Core.Application.Validators;
using Desafio.Core.Application.Validators.Base;
using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Application.Applications;

public class UserApplication : IUserApplication
{
    private readonly IUserService _userService;
    private readonly ISecurityService _securityService;
    private readonly ITokenManager _tokenManager;
    private readonly IMapper _mapper;

    public UserApplication(IUserService userService, ISecurityService securityService, ITokenManager tokenManager, IMapper mapper)
    {
        _userService = userService;
        _securityService = securityService;
        _tokenManager = tokenManager;
        _mapper = mapper;
    }

    public async Task<Response<AuthResponse>> AutheticationAsync(AuthRequest request)
    {
        var validate = new AuthRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return Response.Unprocessable<AuthResponse>(validateErrors.ReportErrors);

        try
        {
            var user = await _userService.GetByLoginAsync(request.Login);
            if (user.ReportErrors.Any())
                return Response.Unprocessable<AuthResponse>(user.ReportErrors);

            var isAuthenticated = await _userService.AuthenticationAsync(request.Password, user.Data.PasswordHash);
            if (!isAuthenticated.Data)
                return Response.Unprocessable<AuthResponse>(ReportError.Create("Password is incorrect."));

            var response = await _tokenManager.GenerateTokenAsync(user.Data);
            return Response.OK(response);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable<AuthResponse>(responseError);
        }
    }

    public async Task<Response<CreateUserResponse>> CreateAsync(CreateUserRequest request)
    {
        var validate = new CreateUserRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return Response.Unprocessable<CreateUserResponse>(validateErrors.ReportErrors);

        try
        {
            var userEntity = _mapper.Map<UserEntity>(request);

            userEntity.PasswordHash = await _securityService.EncryptPassword(request.Password);

            var response = await _userService.CreateAsync(userEntity);
            CreateUserResponse responseOk = new() { Id = response.Data};

            return Response.OK(responseOk);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable<CreateUserResponse>(responseError);
        }
    }

    public async Task<Response> DeleteAsync(int id)
    {
        try
        {
            return await _userService.DeleteAsync(id);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }

    public async Task<Response<List<UserResponse>>> GetAllAsync()
    {
        try
        {
            var result = await _userService.GetAllAsync();

            if (result.ReportErrors.Any())
                return Response.Unprocessable<List<UserResponse>>(result.ReportErrors);

            var response = _mapper.Map<List<UserResponse>>(result.Data);

            return Response.OK(response);
        }
        catch (Exception e)
        {
            List<ReportError> listError = [ReportError.Create(e.Message)];
            return Response.Unprocessable<List<UserResponse>>(listError);
        }
    }

    public async Task<Response> GetByIdAsync(int id)
    {
        try
        {
            var result = await _userService.GetByIdAsync(id);

            var response = _mapper.Map<UserResponse>(result.Data);

            return Response.OK(response);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }

    public async Task<Response> UpdateAsync(UpdateUserRequest request)
    {
        var validate = new UpdateUserRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return validateErrors;

        try
        {
            var userEntity = _mapper.Map<UserEntity>(request);

            userEntity.PasswordHash = await _securityService.EncryptPassword(request.Password);

            return await _userService.UpdateAsync(userEntity);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }
}
