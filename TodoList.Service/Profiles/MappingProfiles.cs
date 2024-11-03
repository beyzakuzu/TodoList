using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Modelss.Dtos.Categories.Requests;
using TodoList.Modelss.Dtos.Categories.Responses;
using TodoList.Modelss.Dtos.Todos.Requests;
using TodoList.Modelss.Dtos.Todos.Responses;
using TodoList.Modelss.Entities;

namespace TodoList.Service.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        
        CreateMap<CreateTodoRequest, Todo>();
        CreateMap<UpdateTodoRequest, Todo>();
        CreateMap<Todo, TodoResponseDto>()
            .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Category.Name)) 
            .ForMember(x => x.UserName, opt => opt.MapFrom(y => y.User.UserName));


        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<Category, CategoryResponseDto>();

    }
}