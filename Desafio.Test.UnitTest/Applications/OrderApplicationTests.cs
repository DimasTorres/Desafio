using AutoMapper;
using Desafio.Core.Application.Applications;
using Desafio.Core.Application.Contracts.Order.Request;
using Desafio.Core.Application.Contracts.Order.Response;
using Desafio.Core.Application.Interfaces.Services;
using Desafio.Core.Application.Interfaces.Strategies;
using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;
using NSubstitute;

namespace Desafio.Test.UnitTest.Applications;

public class OrderApplicationTests
{
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;
    private readonly IProductService _productService;
    private readonly IOrderItemService _orderItemService;
    private readonly ITotalAmountStrategy _totalAmountStrategy;
    private readonly IMapper _mapper;
    private readonly OrderApplication _orderApplication;

    public OrderApplicationTests()
	{
        _orderService = Substitute.For<IOrderService>();
        _userService = Substitute.For<IUserService>();
        _productService = Substitute.For<IProductService>();
        _orderItemService = Substitute.For<IOrderItemService>();
        _totalAmountStrategy = Substitute.For<ITotalAmountStrategy>();
        _mapper = Substitute.For<IMapper>();

        _orderApplication = new OrderApplication(
            _orderService,
            _userService,
            _productService,
            _orderItemService,
            _totalAmountStrategy,
            _mapper);
    }

    [Fact]
    public async Task CreateAsync_Should_Return_Unprocessable_When_Validation_Fails()
    {
        // Arrange
        var request = new CreateOrderRequest(); // invalid request

        // Act
        var response = await _orderApplication.CreateAsync(request);

        // Assert
        Assert.True(response.ReportErrors.Any());
    }

    [Fact]
    public async Task CreateAsync_Should_Return_Unprocessable_When_Product_Not_Found()
    {
        // Arrange
        var request = new CreateOrderRequest
        {
            OrderItems = new List<CreateOrderItemRequest>
            {
                new CreateOrderItemRequest { ProductId = 1 }
            }
        };

        _productService.GetByIdAsync(Arg.Any<int>()).Returns(Task.FromResult(new Response<ProductEntity>()));

        // Act
        var response = await _orderApplication.CreateAsync(request);

        // Assert        
        Assert.True(response.ReportErrors.Any());
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_Orders_With_TotalAmount_Calculated()
    {
        // Arrange
        var orders = new List<OrderEntity>
        {
            new OrderEntity { OrderItems = new List<OrderItemEntity>() }
        };

        _orderService.GetAllAsync().Returns(Task.FromResult(new Response<List<OrderEntity>>(orders)));

        _mapper.Map<List<OrderResponse>>(Arg.Any<List<OrderEntity>>())
            .Returns(new List<OrderResponse> { new OrderResponse { OrderItems = new List<OrderItemResponse>() } });

        _totalAmountStrategy.CalculateTotalAmount(Arg.Any<List<OrderItemEntity>>()).Returns(100m);

        // Act
        var response = await _orderApplication.GetAllAsync();

        // Assert
        Assert.NotNull(response.Data);
        Assert.False(response.ReportErrors.Any());
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Order_With_TotalAmount_Calculated()
    {
        // Arrange
        var order = new OrderEntity { OrderItems = new List<OrderItemEntity>() };

        _orderService.GetByIdAsync(Arg.Any<int>()).Returns(Task.FromResult(new Response<OrderEntity>(order)));

        _mapper.Map<OrderResponse>(Arg.Any<OrderEntity>())
            .Returns(new OrderResponse { OrderItems = new List<OrderItemResponse>() });

        _totalAmountStrategy.CalculateTotalAmount(Arg.Any<List<OrderItemEntity>>()).Returns(50m);

        // Act
        var response = await _orderApplication.GetByIdAsync(1);

        // Assert
        Assert.False(response.ReportErrors.Any());
    }
}