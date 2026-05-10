using TodoList.Application.DTOs;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Services;

public class TodoItemService : ITodoItemService
{
    private readonly ITodoItemRepository _repository;
    private readonly IUserRepository _userRepository;

    public TodoItemService(
        ITodoItemRepository repository,
        IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<TodoItemResponseDto>> GetAllByUserIdAsync(Guid userId)
    {
        var items = await _repository.GetAllTasksByUserIdAsync(userId);
        return items.Select(MapToResponseDto);
    }

    public async Task<TodoItemResponseDto?> GetByIdAsync(int id, Guid userId)
    {
        var item = await _repository.GetTaskByIdAsync(id, userId);
        return item is null ? null : MapToResponseDto(item);
    }

    public async Task<TodoItemResponseDto> CreateAsync(CreateTodoItemDto createDto)
    {
        // Validamos que el usuario existe antes de crear la tarea
        var userExists = await _userRepository.UserExistsAsync(createDto.UserId);
        if (!userExists)
            throw new InvalidOperationException($"El usuario con Id '{createDto.UserId}' no existe.");

        var todoItem = new TodoItem
        {
            Title = createDto.Title.Trim(),
            Description = createDto.Description?.Trim(),
            IsCompleted = false,
            UserId = createDto.UserId
        };

        var created = await _repository.AddTaskAsync(todoItem);
        return MapToResponseDto(created);
    }

    public async Task<TodoItemResponseDto?> UpdateAsync(int id, Guid userId, UpdateTodoItemDto updateDto)
    {
        var todoItem = await _repository.GetTaskByIdAsync(id, userId);
        if (todoItem is null) return null;

        todoItem.Title = updateDto.Title.Trim();
        todoItem.Description = updateDto.Description?.Trim();
        todoItem.IsCompleted = updateDto.IsCompleted;

        await _repository.UpdateTaskAsync(todoItem);
        return MapToResponseDto(todoItem);
    }

    public async Task<bool> DeleteAsync(int id, Guid userId)
    {
        if (!await _repository.TaskExistsAsync(id, userId)) return false;
        await _repository.DeleteTaskAsync(id, userId);
        return true;
    }

    private static TodoItemResponseDto MapToResponseDto(TodoItem item) => new()
    {
        Id = item.Id,
        Title = item.Title,
        Description = item.Description,
        IsCompleted = item.IsCompleted,
        CreatedAt = item.CreatedAt,
        UserId = item.UserId
    };
}

