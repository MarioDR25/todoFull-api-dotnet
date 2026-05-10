namespace TodoList.Domain.common;

public abstract class BaseEntity<TId>
{
    public TId Id { get; set; } = default!;
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    protected BaseEntity(){}
}


