using TodoList.Application.DTOs;

namespace TodoList.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllAsync();
    Task<UserResponseDto?> GetByIdAsync(Guid id);
     Task<UserResponseDto?> UpdateAsync(Guid id, UpdateUserDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}
