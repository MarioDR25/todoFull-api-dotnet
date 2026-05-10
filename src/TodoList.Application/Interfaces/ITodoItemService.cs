using TodoList.Application.DTOs;

namespace TodoList.Application.Interfaces;

public interface ITodoItemService
{
     Task<IEnumerable<TodoItemResponseDto>> GetAllByUserIdAsync(Guid userId);
    Task<TodoItemResponseDto?> GetByIdAsync(int id, Guid userId);
    Task<TodoItemResponseDto> CreateAsync(CreateTodoItemDto createDto);
    Task<TodoItemResponseDto?> UpdateAsync(int id, Guid userId, UpdateTodoItemDto updateDto);
    Task<bool> DeleteAsync(int id, Guid userId);
}
