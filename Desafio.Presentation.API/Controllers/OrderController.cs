using Desafio.Core.Application.Contracts.Order.Request;
using Desafio.Core.Application.Interfaces.Applications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Presentation.API.Controllers;

[Route("api/order")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderApplication _application;

    public OrderController(IOrderApplication application)
    {
        _application = application;
    }

    /// <summary>
    /// Get Order by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Order</returns>
    [HttpGet("id")]
    public async Task<ActionResult> GetById([FromQuery] int id)
    {
        var result = await _application.GetByIdAsync(id);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Get Orders
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="clientId"></param>
    /// <param name="userId"></param>
    /// <returns>List of Order</returns>
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var result = await _application.GetAllAsync();

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Create Order
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Status Code</returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var result = await _application.CreateAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }
}