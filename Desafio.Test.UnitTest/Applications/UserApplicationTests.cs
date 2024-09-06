using AutoMapper;
using Desafio.Core.Application.Applications;
using Desafio.Core.Application.Contracts.User.Request;
using Desafio.Core.Application.Contracts.User.Response;
using Desafio.Core.Application.Interfaces.Applications;
using Desafio.Core.Application.Interfaces.Services;
using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Test.UnitTest.Applications;

public class UserApplicationTests
{
    private readonly IUserService _userService;
    private readonly ISecurityService _securityService;
    private readonly ITokenManager _tokenManager;
    private readonly IMapper _mapper;
    private readonly UserApplication _userApplication;

    public UserApplicationTests()
    {
        _userService = Substitute.For<IUserService>();
        _securityService = Substitute.For<ISecurityService>();
        _tokenManager = Substitute.For<ITokenManager>();
        _mapper = Substitute.For<IMapper>();

        _userApplication = new UserApplication(_userService, _securityService, _tokenManager, _mapper);
    }

    [Fact]
    public async Task AutheticationAsync_Should_Return_Unprocessable_When_Validation_Fails()
    {
        // Arrange
        var request = new AuthRequest(); // invalid request

        // Act
        var response = await _userApplication.AutheticationAsync(request);

        // Assert
        Assert.True(response.ReportErrors.Any());
    }

    [Fact]
    public async Task AutheticationAsync_Should_Return_Unprocessable_When_User_Not_Found()
    {
        // Arrange
        var request = new AuthRequest { Login = "testuser", Password = "password" };
        _userService.GetByLoginAsync(request.Login).Returns(Task.FromResult(new Response<UserEntity>(null, new List<ReportError> { ReportError.Create("User not found") })));

        // Act
        var response = await _userApplication.AutheticationAsync(request);

        // Assert
        Assert.True(response.ReportErrors.Any());
    }

    [Fact]
    public async Task AutheticationAsync_Should_Return_Unprocessable_When_Password_Is_Incorrect()
    {
        // Arrange
        var request = new AuthRequest { Login = "testuser", Password = "wrongpassword" };
        var userEntity = new UserEntity { Id = 1, PasswordHash = "hashedpassword" };

        _userService.GetByLoginAsync(request.Login).Returns(Task.FromResult(new Response<UserEntity>(userEntity)));
        _userService.AuthenticationAsync(request.Password, userEntity.PasswordHash).Returns(Task.FromResult(new Response<bool>(false)));

        // Act
        var response = await _userApplication.AutheticationAsync(request);

        // Assert
        Assert.True(response.ReportErrors.Any());
    }

    [Fact]
    public async Task AutheticationAsync_Should_Authenticate_User_Successfully()
    {
        // Arrange
        var request = new AuthRequest { Login = "testuser", Password = "Correctpassword1@" };
        var userEntity = new UserEntity { Id = 1, PasswordHash = "hashedpassword" };

        _userService.GetByLoginAsync(request.Login).Returns(Task.FromResult(new Response<UserEntity>(userEntity)));
        _userService.AuthenticationAsync(request.Password, userEntity.PasswordHash).Returns(Task.FromResult(new Response<bool>(true)));
        _tokenManager.GenerateTokenAsync(userEntity).Returns(Task.FromResult(new AuthResponse { Token = "token", ExpireIn = 1, Type = "Bearer" }));

        // Act
        var response = await _userApplication.AutheticationAsync(request);

        // Assert
        Assert.False(response.ReportErrors.Any());
    }

    [Fact]
    public async Task CreateAsync_Should_Return_Unprocessable_When_Validation_Fails()
    {
        // Arrange
        var request = new CreateUserRequest(); // invalid request

        // Act
        var response = await _userApplication.CreateAsync(request);

        // Assert
        Assert.True(response.ReportErrors.Any());
    }

    [Fact]
    public async Task CreateAsync_Should_Create_User_Successfully()
    {
        // Arrange
        var request = new CreateUserRequest { Name = "Test User", Login = "testuser", Password = "Password123!", ConfirmPassword = "Password123!" , Email = "test.user@test.com" };
        var userEntity = new UserEntity { Id = 1, Login = "testuser" };

        _mapper.Map<UserEntity>(request).Returns(userEntity);
        _securityService.EncryptPassword(request.Password).Returns(Task.FromResult("encryptedPassword"));
        _userService.CreateAsync(userEntity).Returns(Task.FromResult(new Response<int>(1)));

        // Act
        var response = await _userApplication.CreateAsync(request);

        // Assert
        Assert.False(response.ReportErrors.Any());
    }

    [Fact]
    public async Task DeleteAsync_Should_Delete_User_Successfully()
    {
        // Arrange
        var user = new UserEntity { Id = 1 };
        _userService.DeleteAsync(1).Returns(Task.FromResult(new Response()));

        // Act
        var response = await _userApplication.DeleteAsync(1);

        // Assert
        Assert.False(response.ReportErrors.Any());
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_Users()
    {
        // Arrange
        var users = new List<UserEntity> { new UserEntity { Id = 1, Login = "testuser" } };
        _userService.GetAllAsync().Returns(Task.FromResult(new Response<List<UserEntity>>(users)));
        _mapper.Map<List<UserResponse>>(users).Returns(new List<UserResponse> { new UserResponse() });

        // Act
        var response = await _userApplication.GetAllAsync();

        // Assert
        Assert.NotNull(response.Data);
        Assert.False(response.ReportErrors.Any());
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_User()
    {
        // Arrange
        var user = new UserEntity { Id = 1, Login = "testuser" };
        _userService.GetByIdAsync(1).Returns(Task.FromResult(new Response<UserEntity>(user)));

        // Act
        var response = await _userApplication.GetByIdAsync(1);

        // Assert
        Assert.False(response.ReportErrors.Any());
    }

    [Fact]
    public async Task UpdateAsync_Should_Return_Unprocessable_When_Validation_Fails()
    {
        // Arrange
        var request = new UpdateUserRequest(); // invalid request

        // Act
        var response = await _userApplication.UpdateAsync(request);

        // Assert
        Assert.True(response.ReportErrors.Any());
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_User_Successfully()
    {
        // Arrange
        var request = new UpdateUserRequest { Id = 1, Name = "Updated User", Login = "UpdatedUser", Password = "NewPassword123!", ConfirmPassword = "NewPassword123!", Email = "test.user@test.com" };
        var userEntity = new UserEntity { Id = 1, Login = "testuser" };

        _mapper.Map<UserEntity>(request).Returns(userEntity);
        _securityService.EncryptPassword(request.Password).Returns(Task.FromResult("newEncryptedPassword"));
        _userService.UpdateAsync(userEntity).Returns(Task.FromResult(new Response()));

        // Act
        var response = await _userApplication.UpdateAsync(request);

        // Assert
        Assert.False(response.ReportErrors.Any());
    }
}
