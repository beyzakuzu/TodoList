using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Entities;

namespace TodoList.DataAccess.Abstracts
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<List<Todo>> GetAllTodosByCategoryIdAsync(int categoryId);
    }
}
