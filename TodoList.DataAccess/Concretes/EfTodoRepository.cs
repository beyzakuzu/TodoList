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

namespace TodoList.DataAccess.Concretes;


    public class EfTodoRepository : EfRepositoryBase<BaseDbContext, Todo, Guid>, ITodoRepository
    {
        public EfTodoRepository(BaseDbContext context) : base(context)
        {
        }

        public async Task<List<Todo>> GetAllByCategoryIdAsync(int categoryId)
        {
            return await Context.Set<Todo>().Where(todo => todo.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<Todo>> GetAllByUserIdAsync(string userId)
        {
            return await Context.Set<Todo>().Where(todo => todo.UserId == userId).ToListAsync();
        }

        public async Task<List<Todo>> GetAllByPriorityAsync(Priority priority)
        {
            return await Context.Set<Todo>().Where(todo => todo.Priority == priority).ToListAsync();
        }

        public async Task<List<Todo>> GetAllByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await Context.Set<Todo>()
                .Where(todo => todo.StartDate >= startDate && todo.EndDate <= endDate)
                .ToListAsync();
        }
    }

