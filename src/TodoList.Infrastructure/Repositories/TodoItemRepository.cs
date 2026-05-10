using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Infrastructure.Data;

namespace TodoList.Infrastructure.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly AppDbContext _context;

    public TodoItemRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<TodoItem>> GetAllTasksByUserIdAsync(Guid userId)
    {
        return await _context.TodoItems
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<TodoItem?> GetTaskByIdAsync(int id, Guid userId)
    {
        return await _context.TodoItems
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
    }

    public async Task<TodoItem> AddTaskAsync(TodoItem todoItem)
    {
        await _context.TodoItems.AddAsync(todoItem);
        await _context.SaveChangesAsync();
        return todoItem;
    }

    public async Task UpdateTaskAsync(TodoItem todoItem)
    {
        _context.TodoItems.Update(todoItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(int id, Guid userId)
    {
        TodoItem? todoItem = await _context.TodoItems
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

        if (todoItem is not null)
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }
    }


    public async Task<bool> TaskExistsAsync(int id, Guid userId)
    {
        return await _context.TodoItems
            .AnyAsync(t => t.Id == id && t.UserId == userId);
    }



}