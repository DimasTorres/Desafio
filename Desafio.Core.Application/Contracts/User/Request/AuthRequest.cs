namespace Desafio.Core.Application.Contracts.User.Request;

public sealed class AuthRequest
{
    public string Login { get; set; }
    public string Password { get; set; }
}