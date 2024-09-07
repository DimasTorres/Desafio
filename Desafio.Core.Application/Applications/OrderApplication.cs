using AutoMapper;
using Desafio.Core.Application.Interfaces.Applications;
using Desafio.Core.Application.Interfaces.Services;
using Desafio.Core.Application.Validators.Base;
using Desafio.Core.Application.Contracts.Order.Request;
using Desafio.Core.Application.Contracts.Order.Response;
using Desafio.Core.Application.Validators;
using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;
using Desafio.Core.Application.Interfaces.Strategies;

namespace Desafio.Core.Application.Applications;

public class OrderApplication : IOrderApplication
{
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;
    private readonly IProductService _productService;
    private readonly IOrderItemService _orderItemService;
    private readonly ICalculateStrategy _totalAmountStrategy;
    private readonly IMapper _mapper;

    public OrderApplication(
        IOrderService orderService,
        IUserService userService,
        IProductService productService,
        IOrderItemService orderItemService,
        ICalculateStrategy totalAmountStrategy,
        IMapper mapper)
    {
        _orderService = orderService;
        _userService = userService;
        _productService = productService;
        _orderItemService = orderItemService;
        _totalAmountStrategy = totalAmountStrategy;
        _mapper = mapper;
    }

    public async Task<Response<CreateOrderResponse>> CreateAsync(CreateOrderRequest request)
    {
        var validate = new CreateOrderRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return Response.Unprocessable<CreateOrderResponse>(validateErrors.ReportErrors);

        try
        {
            List<OrderItemEntity> orderItems = new();
            foreach (var item in request.OrderItems)
            {
                var orderItem = _mapper.Map<OrderItemEntity>(item);
                orderItems.Add(orderItem);
            }

            var orderEntity = new OrderEntity()
            {
                ClientName = request.ClientName,
                ClientEmail = request.ClientEmail,
                IsPaid = request.IsPaid,
                UserId = request.UserId,
                OrderItems = orderItems
            };

            var response = await _orderService.CreateAsync(orderEntity);
            CreateOrderResponse responseOk = new() { Id = response.Data };

            return Response.OK(responseOk);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable<CreateOrderResponse>(responseError);
        }
    }
    public async Task<Response<List<OrderResponse>>> GetAllAsync()
    {
        try
        {
            List<ReportError> reportErrors = new();
            var result = await _orderService.GetAllAsync();

            if (result.ReportErrors.Any())
                return Response.Unprocessable<List<OrderResponse>>(result.ReportErrors);

            var response = _mapper.Map<List<OrderResponse>>(result.Data);

            foreach (var order in response)
            {
                order.TotalAmount = _totalAmountStrategy.CalculateTotalAmount(order.OrderItems);
            }

            return Response.OK(response);
        }
        catch (Exception e)
        {
            List<ReportError> listError = [ReportError.Create(e.Message)];
            return Response.Unprocessable<List<OrderResponse>>(listError);
        }
    }

    public async Task<Response> GetByIdAsync(int id)
    {
        try
        {
            var result = await _orderService.GetByIdAsync(id);

            var response = _mapper.Map<OrderResponse>(result.Data);

            //Aqui poderia ser utilizado o CalculateTotalAmount(List<OrderItemResponse> orderItems)
            //ao invés do CalculateTotalAmount(List<OrderItemEntity> orderItems)
            //porém deixei assim implementado para exemplificar a utilização do Design Pattern Strategy
            response.TotalAmount = _totalAmountStrategy.CalculateTotalAmount(result.Data.OrderItems); ;

            return Response.OK(response);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }    
}