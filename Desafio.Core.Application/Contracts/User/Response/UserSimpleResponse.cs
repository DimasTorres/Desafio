namespace Desafio.Core.Application.Contracts.User.Response;

public sealed class UserSimpleResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}