using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoList.Modelss.Dtos.Todos.Requests;
using TodoList.Modelss.Entities;
using static TodoList.Service.Absracts.ITodoService;
[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet("getall")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        string userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        var result = await _todoService.GetAllAsync(userId, userRole);

        return Ok(result);
    }

    [HttpPost("add")]
    [Authorize] 
    public async Task<IActionResult> Add([FromBody] CreateTodoRequest dto)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var result = await _todoService.AddAsync(dto, userId);
        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
    }

    [HttpGet("getbyid/{id}")]
    [Authorize] 
    public async Task<IActionResult> GetById(Guid id)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        string userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value; 
        var result = await _todoService.GetByIdAsync(id, userId, userRole);
        return Ok(result);
    }

    [HttpPut("update")]
    [Authorize] 
    public async Task<IActionResult> Update([FromBody] UpdateTodoRequest dto)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        string userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value; 
        var result = await _todoService.UpdateAsync(dto, userId, userRole);
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    [Authorize] 
    public async Task<IActionResult> Delete(Guid id)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        string userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value; 
        var result = await _todoService.DeleteAsync(id, userId, userRole);
        return NoContent();
    }

    [HttpGet("getallbycategoryid/{categoryId}")]
    [Authorize]
    public async Task<IActionResult> GetAllByCategoryId(int categoryId)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        string userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

        var result = await _todoService.GetAllByCategoryIdAsync(categoryId, userId, userRole);

        return Ok(result);
    }

    [HttpGet("getallbyuserid/{userId}")]
    [Authorize] 
    public async Task<IActionResult> GetAllByUserId(string userId)
    {
        string requesterId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value; 
        string userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value; 
        var result = await _todoService.GetAllByUserIdAsync(userId, requesterId, userRole);
        return Ok(result);
    }

    [HttpGet("getallbypriority/{priority}")]
    [Authorize] 
    public async Task<IActionResult> GetAllByPriority(Priority priority)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        string userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value; 
        var result = await _todoService.GetAllByPriorityAsync(priority, userId, userRole);
        return Ok(result);
    }

    [HttpGet("getallbydaterange")]
    [Authorize] 
    public async Task<IActionResult> GetAllByDateRange(DateTime startDate, DateTime endDate)
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        string userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value; 
        var result = await _todoService.GetAllByDateRangeAsync(startDate, endDate, userId, userRole);
        return Ok(result);
    }
}
