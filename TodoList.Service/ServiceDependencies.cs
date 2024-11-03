using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;


using System.Reflection;
using TodoList.Service.Absracts;
using TodoList.Service.Concretes;
using TodoList.Service.Profiles;


namespace SweetDictionary.Service;

public static class ServiceDependencies
{

    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfiles));

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();    
        services.AddScoped<ITodoService, TodoService>();    
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}