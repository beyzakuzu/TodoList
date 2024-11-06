using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TodoList.Modelss.Dtos.Todos.Requests;
using TodoList.Modelss.Dtos.Todos.Responses;
using TodoList.Modelss.Entities;

namespace TodoList.Service.Absracts
{
    public interface ITodoService
    {
        Task<ReturnModel<NoData>> AddAsync(CreateTodoRequest dto);
        Task<ReturnModel<NoData>> UpdateAsync(UpdateTodoRequest dto);
        Task<ReturnModel<NoData>> DeleteAsync(Guid id);
        Task<ReturnModel<TodoResponseDto>> GetByIdAsync(Guid id);
        Task<ReturnModel<List<TodoResponseDto>>> GetAllByUserIdAsync(string userId);
        Task<ReturnModel<List<TodoResponseDto>>> GetAllByPriorityAsync(Priority priority);
        Task<ReturnModel<List<TodoResponseDto>>> GetAllByStartDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<ReturnModel<NoData>> DeleteExpiredTodosAsync();
    }
}
