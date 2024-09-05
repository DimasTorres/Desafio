using Desafio.Core.Domain.Common;

namespace Desafio.Core.Domain.Entities;

public class UserEntity : EntityCommon
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
}
