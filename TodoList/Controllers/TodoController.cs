using Core.Tokens.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoList.Modelss.Dtos.Todos.Requests;
using TodoList.Modelss.Entities;
using TodoList.Service.Absracts;
using static TodoList.Service.Absracts.ITodoService;
[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly DecoderService _decoderService;

    public TodoController(ITodoService todoService, DecoderService decoderService)
    {
        _todoService = todoService;
        _decoderService = decoderService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddAsync([FromBody] CreateTodoRequest dto)
    {
        var result = await _todoService.AddAsync(dto);
        return StatusCode(result.Status, result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateTodoRequest dto)
    {
        var result = await _todoService.UpdateAsync(dto);
        return StatusCode(result.Status, result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
    {
        var result = await _todoService.DeleteAsync(id);
        return StatusCode(result.Status, result);
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] Guid id)
    {
        var result = await _todoService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("getallbypriority")]
    public async Task<IActionResult> GetAllByPriorityAsync([FromQuery] Priority priority)
    {
        var result = await _todoService.GetAllByPriorityAsync(priority);
        return Ok(result);
    }

    [HttpGet("getallbystartdaterange")]
    public async Task<IActionResult> GetAllByStartDateRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var result = await _todoService.GetAllByStartDateRangeAsync(startDate, endDate);
        return Ok(result);
    }

 
    [HttpGet("owntodos")]
    public async Task<IActionResult> OwnTodos()
    {
        var userId = _decoderService.GetUserId();
        var result = await _todoService.GetAllByUserIdAsync(userId); 
        return Ok(result); 
    }

    [HttpDelete("deleteexpired")]
    public async Task<IActionResult> DeleteExpiredTodosAsync()
    {
        var result = await _todoService.DeleteExpiredTodosAsync();
        return StatusCode(result.Status, result);
    }
}

