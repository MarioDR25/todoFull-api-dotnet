using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Application.DTOs;

public class UpdateTodoItemDto
{
    [Required(ErrorMessage = "The Title is required.")]
    [MaxLength(20, ErrorMessage = "The title cannot exceed 20 characters.")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(200, ErrorMessage = "The description cannot exceed 200 characters.")]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }
}


