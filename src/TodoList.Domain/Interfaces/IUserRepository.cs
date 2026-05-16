using TodoList.Domain.Entities;

namespace TodoList.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddUserAsync(User user);
    Task UpdateUserAsync(User user);    
    Task DeleteUserAsync(Guid id);
    Task<bool> ExistsAsync(Guid id); 
    Task<bool> EmailExistsAsync(string email);
    Task<bool> UserExistsAsync(string username);
}
