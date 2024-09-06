namespace Desafio.Core.Application.Contracts.User.Response;

public sealed class UserResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
}