

using System.ComponentModel.DataAnnotations;

namespace TodoList.Application.DTOs;

public class CreateTodoItemDto
{
    [Required(ErrorMessage = "The Title is required.")]
    [MaxLength(20, ErrorMessage = "The title cannot exceed 20 characters.")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(200, ErrorMessage = "The description cannot exceed 200 characters.")]
    public string? Description { get; set; }

    
     /* El UserId viene en el DTO de entrada porque aún no tengo autenticación JWT.
     con JWT, el UserId se extraería del token */
    
    [Required(ErrorMessage = "The UserId is required.")]
    public Guid UserId { get; set; }
}


