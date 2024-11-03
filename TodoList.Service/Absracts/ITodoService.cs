using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Dtos.Todos.Requests;
using TodoList.Modelss.Dtos.Todos.Responses;
using TodoList.Modelss.Entities;

namespace TodoList.Service.Absracts
{
    public interface ITodoService
    {
        public interface ITodoService
        {
            Task<ReturnModel<TodoResponseDto>> AddAsync(CreateTodoRequest dto, string userId);
            Task<ReturnModel<List<TodoResponseDto>>> GetAllAsync(string userId, string userRole);
            Task<ReturnModel<TodoResponseDto>> GetByIdAsync(Guid id, string userId, string userRole);
            Task<ReturnModel<TodoResponseDto>> UpdateAsync(UpdateTodoRequest dto, string userId, string userRole);
            Task<ReturnModel<string>> DeleteAsync(Guid id, string userId, string userRole);
            Task<ReturnModel<List<TodoResponseDto>>> GetAllByCategoryIdAsync(int categoryId, string userId, string userRole);
            Task<ReturnModel<List<TodoResponseDto>>> GetAllByUserIdAsync(string userId, string requesterId, string userRole);
            Task<ReturnModel<List<TodoResponseDto>>> GetAllByPriorityAsync(Priority priority, string userId, string userRole);
            Task<ReturnModel<List<TodoResponseDto>>> GetAllByDateRangeAsync(DateTime startDate, DateTime endDate, string userId, string userRole);
        }

    }
}
