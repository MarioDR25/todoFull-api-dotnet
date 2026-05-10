using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.DTOs;
using TodoList.Application.Interfaces;

namespace TodoList.Api.Controllers;

[ApiController]
[Route("todos/user/[controller]")]
public class TodoItemsController(ITodoItemService service) : ControllerBase
{
    private readonly ITodoItemService _service = service;


    /// GET api/todoitems?userId={guid}
    /// Las tareas siempre se filtran por usuario.
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TodoItemResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TodoItemResponseDto>>> GetAll([FromQuery][Required] Guid userId)
    {
        var items = await _service.GetAllByUserIdAsync(userId);
        return Ok(items);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TodoItemResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItemResponseDto>> GetById(int id, [FromQuery][Required] Guid userId)
    {
        var item = await _service.GetByIdAsync(id, userId);
        if (item is null)
            return NotFound(new { message = $"No se encontró la tarea con Id {id}." });

        return Ok(item);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TodoItemResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItemResponseDto>> Create([FromBody] CreateTodoItemDto createDto)
    {
        try
        {
            var createdItem = await _service.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById),
                new { id = createdItem.Id, userId = createdItem.UserId },
                createdItem);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TodoItemResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoItemResponseDto>> Update(
        int id,
        [FromQuery][Required] Guid userId,
        [FromBody] UpdateTodoItemDto updateDto)
    {
        var updatedItem = await _service.UpdateAsync(id, userId, updateDto);
        if (updatedItem is null)
            return NotFound(new { message = $"No se encontró la tarea con Id {id}." });

        return Ok(updatedItem);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, [FromQuery][Required] Guid userId)
    {
        var deleted = await _service.DeleteAsync(id, userId);
        if (!deleted)
            return NotFound(new { message = $"No se encontró la tarea con Id {id}." });

        return NoContent();
    }
}