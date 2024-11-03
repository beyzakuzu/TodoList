using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Entities;

namespace TodoList.DataAccess.Abstracts
{
    public interface ITodoRepository : IRepository<Todo, Guid>
    {
     
        Task<List<Todo>> GetAllByCategoryIdAsync(int categoryId);
        Task<List<Todo>> GetAllByUserIdAsync(string userId);
        Task<List<Todo>> GetAllByPriorityAsync(Priority priority);
        Task<List<Todo>> GetAllByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
