using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Modelss.Dtos.Categories.Requests;
using TodoList.Service.Absracts;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _categoryService.GetAllAsync();
        return Ok(result);
    }

    [HttpPost("add")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add([FromBody] CreateCategoryRequest dto)
    {
        var result = await _categoryService.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
    }

    [HttpGet("getbyid/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _categoryService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest dto)
    {
        var result = await _categoryService.UpdateAsync(dto);
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _categoryService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("getalltodobycategoryid/{categoryId}")]
    public async Task<IActionResult> GetAllTodosByCategoryId(int categoryId)
    {
        var result = await _categoryService.GetAllTodosByCategoryIdAsync(categoryId);
        return Ok(result);
    }
}
