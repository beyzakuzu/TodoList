using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Dtos.Categories.Requests;
using TodoList.Modelss.Dtos.Categories.Responses;
using TodoList.Modelss.Entities;

namespace TodoList.Service.Absracts
{
    public interface ICategoryService
    {
        Task<ReturnModel<CategoryResponseDto>> AddAsync(CreateCategoryRequest dto);
        Task<ReturnModel<List<CategoryResponseDto>>> GetAllAsync();
        Task<ReturnModel<CategoryResponseDto>> GetByIdAsync(int id);
        Task<ReturnModel<CategoryResponseDto>> UpdateAsync(UpdateCategoryRequest dto);
        Task<ReturnModel<string>> DeleteAsync(int id);
        Task<ReturnModel<List<Todo>>> GetAllTodosByCategoryIdAsync(int categoryId);
    }
}
