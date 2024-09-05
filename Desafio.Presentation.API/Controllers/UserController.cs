using Desafio.Core.Application.Contracts.User.Request;
using Desafio.Core.Application.Interfaces.Applications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Presentation.API.Controllers;

[Route("api/user")]
[ApiController]
//[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserApplication _application;

    public UserController(IUserApplication application)
    {
        _application = application;
    }

    /// <summary>
    /// Authentication
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Token</returns>
    [AllowAnonymous]
    [HttpPost("auth")]
    public async Task<ActionResult> AuthenticateUser([FromBody] AuthRequest request)
    {
        var result = await _application.AutheticationAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }


    /// <summary>
    /// Get User by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>User</returns>
    [HttpGet("id")]
    public async Task<ActionResult> GetById([FromQuery] int id)
    {
        var result = await _application.GetByIdAsync(id);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Get Users
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns>List of User</returns>
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var result = await _application.GetAllAsync();

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Create User
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Status Code</returns>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateUserRequest request)
    {
        var result = await _application.CreateAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Update User
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Status Code</returns>
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateUserRequest request)
    {
        var result = await _application.UpdateAsync(request);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }

    /// <summary>
    /// Delete User
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Status Code</returns>
    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] int id)
    {
        var result = await _application.DeleteAsync(id);

        if (result.ReportErrors.Any())
            return UnprocessableEntity(result.ReportErrors);

        return Ok(result);
    }
}