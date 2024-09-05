using AutoMapper;
using Desafio.Core.Application.Interfaces.Applications;
using Desafio.Core.Application.Interfaces.Services;
using Desafio.Core.Application.Validators.Base;
using Desafio.Core.Application.Contracts.Order.Request;
using Desafio.Core.Application.Contracts.Order.Response;
using Desafio.Core.Application.Validators;
using Desafio.Core.Domain.Common;
using Desafio.Core.Domain.Entities;

namespace Desafio.Core.Application.Applications;

public class OrderApplication : IOrderApplication
{
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;
    private readonly IProductService _productService;
    private readonly IOrderItemService _orderItemService;
    private readonly IMapper _mapper;

    public OrderApplication(
        IOrderService orderService,
        IUserService userService,
        IProductService productService,
        IOrderItemService orderItemService,
        IMapper mapper)
    {
        _orderService = orderService;
        _userService = userService;
        _productService = productService;
        _orderItemService = orderItemService;
        _mapper = mapper;
    }

    public async Task<Response> CreateAsync(CreateOrderRequest request)
    {
        var validate = new CreateOrderRequestValidator();
        var validateErrors = validate.Validate(request).GetErrors();
        if (validateErrors.ReportErrors.Any())
            return validateErrors;

        try
        {
            List<ReportError> reportErrors = new();
            List<OrderItemEntity> orderItems = new();
            foreach (var item in request.OrderItems)
            {
                var itemExist = await _productService.GetByIdAsync(item.ProductId);
                if (itemExist.Data.Id > 0)
                {
                    reportErrors.Add(ReportError.Create($"Product {item.ProductId} not found."));
                }
                else
                {
                    var product = await _productService.GetByIdAsync(item.ProductId);

                    var orderItem = _mapper.Map<OrderItemEntity>(item);
                }
            }

            if (reportErrors.Any())
            {
                return Response.Unprocessable(reportErrors);
            }

            var orderEntity = new OrderEntity()
            {
                ClientName = request.ClientName,
                ClientEmail = request.ClientEmail,
                IsPaid = request.IsPaid,
                UserId = request.UserId,
                OrderItems = orderItems
            };

            return await _orderService.CreateAsync(orderEntity);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
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

            foreach (var item in result.Data)
            {
                var orderItems = await _orderItemService.GetItemByOrderIdAsync(item.Id);
                item.OrderItems = orderItems.Data;
            }

            var response = _mapper.Map<List<OrderResponse>>(result.Data);

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

            return Response.OK(response);
        }
        catch (Exception e)
        {
            var responseError = ReportError.Create(e.Message);
            return Response.Unprocessable(responseError);
        }
    }
}