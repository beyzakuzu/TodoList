using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DataAccess.Abstracts;
using TodoList.DataAccess.Contexts;
using TodoList.Modelss.Entities;

namespace TodoList.DataAccess.Concretes
{
    public class EfCategoryRepository : EfRepositoryBase<BaseDbContext, Category, int>, ICategoryRepository
    {
        public EfCategoryRepository(BaseDbContext context) : base(context)
        {
        }

        public async Task<List<Todo>> GetAllTodosByCategoryIdAsync(int categoryId)
        {
            return await Context.Set<Category>()
                .Where(c => c.Id == categoryId)
                .SelectMany(c => c.Todos)
                .ToListAsync();
        }
    }
}
