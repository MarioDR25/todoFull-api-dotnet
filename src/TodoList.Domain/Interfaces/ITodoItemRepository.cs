using TodoList.Domain.Entities;

namespace TodoList.Domain.Interfaces;

public interface ITodoItemRepository
{
    Task<IEnumerable<TodoItem>> GetAllTasksByUserIdAsync(Guid userId);
    Task<TodoItem?> GetTaskByIdAsync(int id, Guid userId);
    Task<TodoItem> AddTaskAsync(TodoItem todoItem);
    Task UpdateTaskAsync(TodoItem todoItem);
    Task DeleteTaskAsync(int id, Guid userId);
    Task<bool> TaskExistsAsync(int id, Guid userId);
}
