using System.ComponentModel.DataAnnotations;

namespace TodoList.Application.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "The email is required.")]
    [EmailAddress(ErrorMessage = "The email format is invalid.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Password is required.")]
    public string Password { get; set; } = string.Empty;
}


