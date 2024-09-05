namespace Desafio.Core.Domain.Common;

public abstract class EntityCommon : EntityIdCommon
{
    public DateTime CreateAt { get; set; }
    public bool IsDeleted { get; set; }
}
