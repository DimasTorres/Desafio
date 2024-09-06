using System.ComponentModel.DataAnnotations;

namespace Desafio.Core.Domain.Common;

public abstract class EntityIdCommon
{
    [Key]
    public int Id { get; set; }
}
