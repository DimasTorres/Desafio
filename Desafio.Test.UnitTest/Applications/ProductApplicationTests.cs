using AutoMapper;
using Desafio.Core.Application.Applications;
using Desafio.Core.Application.Contracts.Product.Request;
using Desafio.Core.Application.Contracts.Product.Response;
using Desafio.Core.Application.Interfaces.Services;
using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;
using NSubstitute;

namespace Desafio.Test.UnitTest.Applications;

public class ProductApplicationTests
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    private readonly ProductApplication _productApplication;

    public ProductApplicationTests()
    {
        _productService = Substitute.For<IProductService>();
        _mapper = Substitute.For<IMapper>();
        _productApplication = new ProductApplication(_productService, _mapper);
    }

    [Fact]
    public async Task CreateAsync_Should_Return_Unprocessable_When_Validation_Fails()
    {
        // Arrange
        var request = new CreateProductRequest(); // invalid request

        // Act
        var response = await _productApplication.CreateAsync(request);

        // Assert
        Assert.True(response.ReportErrors.Any());
    }

    [Fact]
    public async Task CreateAsync_Should_Create_Product_Successfully()
    {
        // Arrange
        var request = new CreateProductRequest { ProductName = "Valid Product" };
        var productEntity = new ProductEntity { Id = 1 };

        _mapper.Map<ProductEntity>(request).Returns(productEntity);
        _productService.CreateAsync(productEntity).Returns(Task.FromResult(new Response<int>(1)));

        // Act
        var response = await _productApplication.CreateAsync(request);

        // Assert
        Assert.NotNull(response.Data);
        Assert.False(response.ReportErrors.Any());
    }

    [Fact]
    public async Task DeleteAsync_Should_Return_Unprocessable_When_Product_Not_Found()
    {
        // Arrange
        _productService.GetByIdAsync(Arg.Any<int>()).Returns(Task.FromResult(new Response<ProductEntity>()));

        // Act
        var response = await _productApplication.DeleteAsync(1);

        // Assert
        Assert.True(response.ReportErrors.Any());
    }

    [Fact]
    public async Task DeleteAsync_Should_Delete_Product_Successfully()
    {
        // Arrange
        var product = new ProductEntity{ Id = 1 };
        _productService.GetByIdAsync(1).Returns(Task.FromResult(new Response<ProductEntity>(product)));
        _productService.DeleteAsync(1).Returns(Task.FromResult(new Response()));

        // Act
        var response = await _productApplication.DeleteAsync(1);

        // Assert
        Assert.False(response.ReportErrors.Any());
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_Products()
    {
        // Arrange
        var products = new List<ProductEntity> { new ProductEntity { Id = 1 } };
        _productService.GetAllAsync().Returns(Task.FromResult(new Response<List<ProductEntity>>(products)));
        _mapper.Map<List<ProductResponse>>(products).Returns(new List<ProductResponse> { new ProductResponse() });

        // Act
        var response = await _productApplication.GetAllAsync();

        // Assert
        Assert.True(response.Data.Any());
        Assert.False(response.ReportErrors.Any());
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Product()
    {
        // Arrange
        var product = new ProductEntity { Id = 1 };
        _productService.GetByIdAsync(1).Returns(Task.FromResult(new Response<ProductEntity>(product)));

        // Act
        var response = await _productApplication.GetByIdAsync(1);

        // Assert
        Assert.False(response.ReportErrors.Any());
    }

    [Fact]
    public async Task UpdateAsync_Should_Return_Unprocessable_When_Validation_Fails()
    {
        // Arrange
        var request = new UpdateProductRequest(); // invalid request

        // Act
        var response = await _productApplication.UpdateAsync(request);

        // Assert
        Assert.True(response.ReportErrors.Any());
    }

    public async Task UpdateAsync_Should_Update_Product_Successfully()
    {
        // Arrange
        var request = new UpdateProductRequest { ProductName = "Updated Product" };
        var productEntity = new ProductEntity { Id = 1 };

        _mapper.Map<ProductEntity>(request).Returns(productEntity);
        _productService.UpdateAsync(productEntity).Returns(Task.FromResult(new Response()));

        // Act
        var response = await _productApplication.UpdateAsync(request);

        // Assert
        Assert.False(response.ReportErrors.Any());
    }
}
