namespace Desafio.Core.Application.Contracts.User.Request;

public sealed class CreateUserRequest
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
}