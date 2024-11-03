using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DataAccess.Abstracts;
using TodoList.DataAccess.Concretes;
using TodoList.DataAccess.Contexts;

namespace TodoList.DataAccess
{
    public static class DataAccessRepositoryDependencies
    {
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
           
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();
            services.AddScoped<ITodoRepository,EfTodoRepository>();

            services.AddDbContext<BaseDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
            return services;
        }
    }
}
