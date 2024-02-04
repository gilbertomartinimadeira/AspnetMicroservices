namespace Ordering.Domain.Common;

public abstract class EntityBase
{
    public int Id { get; private set; }
    public string? CreatedBy { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public string? LastModifiedBy { get; private set; }
    public DateTime LastModifiedDate { get; private set; }
    

}
