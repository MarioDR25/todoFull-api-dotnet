using TodoList.Application.DTOs;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Services;

public class UserService(IUserRepository repository) : IUserService
{
    private readonly IUserRepository _repository = repository;

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        var users = await _repository.GetAllUsersAsync();
        return users.Select(MapToResponseDto);
    }

    public async Task<UserResponseDto?> GetByIdAsync(Guid id)
    {
        var user = await _repository.GetUserByIdAsync(id);
        return user is null ? null : MapToResponseDto(user);
    }


    public async Task<UserResponseDto?> UpdateAsync(Guid id, UpdateUserDto updateDto)
    {
        var user = await _repository.GetUserByIdAsync(id);
        if (user is null) return null;
        user.Username = updateDto.Username.ToLower().Trim();
        user.Email = updateDto.Email.ToLower().Trim();

        await _repository.UpdateUserAsync(user);
        return MapToResponseDto(user);
    }

    
    public async Task<bool> DeleteAsync(Guid id)
    {
        if (!await _repository.ExistsAsync(id)) return false;
        await _repository.DeleteUserAsync(id);
        return true;
    }

    private static UserResponseDto MapToResponseDto(User user) => new()
    {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email,
        CreatedAt = user.CreatedAt,
        
    };
}
