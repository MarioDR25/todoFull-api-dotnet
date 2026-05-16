using TodoList.Application.DTOs;

namespace TodoList.Application.Interfaces;

public interface IUserAuthService
{
    Task<AuthResponseDto> RegisterAsync(CreateUserDto createDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);

}
