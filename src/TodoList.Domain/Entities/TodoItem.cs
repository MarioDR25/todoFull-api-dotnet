using TodoList.Domain.common;

namespace TodoList.Domain.Entities;

public class TodoItem : BaseEntity<int>
{
    public required string Title { get; set; } 
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = false;
    public Guid UserId { get; set; }
    public User? User { get; private set; }
    public TodoItem(){}
    
}


