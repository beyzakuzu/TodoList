using AutoMapper;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using TodoList.DataAccess.Abstracts;
using TodoList.Modelss.Dtos.Todos.Requests;
using TodoList.Modelss.Dtos.Todos.Responses;
using TodoList.Modelss.Entities;
using TodoList.Service.Absracts;
using TodoList.Service.Constants;
using TodoList.Service.Rules;

namespace TodoList.Service.Concretes;

public sealed class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    private readonly TodoBusinessRules _businessRules;

    public TodoService(ITodoRepository todoRepository, IMapper mapper, TodoBusinessRules businessRules)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<ReturnModel<TodoResponseDto>> AddAsync(CreateTodoRequest dto, string userId)
    {
        Todo createdTodo = _mapper.Map<Todo>(dto);
        createdTodo.Id = Guid.NewGuid();
        createdTodo.UserId = userId;

        var todo = await _todoRepository.AddAsync(createdTodo);
        var response = _mapper.Map<TodoResponseDto>(todo);

        return new ReturnModel<TodoResponseDto>
        {
            Data = response,
            Message = Messages.TodoAddedMessage,
            Status = 201,
            Success = true
        };
    }

    public async Task<ReturnModel<List<TodoResponseDto>>> GetAllAsync()
    {
        var todos = await _todoRepository.GetAllAsync();
        var responses = _mapper.Map<List<TodoResponseDto>>(todos);
        return new ReturnModel<List<TodoResponseDto>>
        {
            Data = responses,
            Message = "Tüm todo'lar listelendi.",
            Status = 200,
            Success = true
        };
    }

    public async Task<ReturnModel<TodoResponseDto>> GetByIdAsync(Guid id, string userId, string userRole)
    {
        var todo = await _todoRepository.GetByIdAsync(id);
        _businessRules.TodoIsNullCheck(todo);
        _businessRules.UserCanAccessTodo(todo, userId, userRole); 

        var response = _mapper.Map<TodoResponseDto>(todo);
        return new ReturnModel<TodoResponseDto>
        {
            Data = response,
            Message = "İlgili todo gösterildi.",
            Status = 200,
            Success = true
        };
    }

    public async Task<ReturnModel<TodoResponseDto>> UpdateAsync(UpdateTodoRequest dto, string userId, string userRole)
    {
        var todo = await _todoRepository.GetByIdAsync(dto.Id);
        _businessRules.TodoIsNullCheck(todo);
        _businessRules.UserCanAccessTodo(todo, userId, userRole); 

        todo.Title = dto.Title;
        todo.Description = dto.Description;
        todo.StartDate = dto.StartDate;
        todo.EndDate = dto.EndDate;
        todo.Priority = dto.Priority;
        todo.Completed = dto.Completed;

        await _todoRepository.UpdateAsync(todo);

        var response = _mapper.Map<TodoResponseDto>(todo);

        return new ReturnModel<TodoResponseDto>
        {
            Data = response,
            Message = Messages.TodoUpdatedMessage,
            Status = 200,
            Success = true
        };
    }

    public async Task<ReturnModel<string>> DeleteAsync(Guid id, string userId, string userRole)
    {
        var todo = await _todoRepository.GetByIdAsync(id);
        _businessRules.TodoIsNullCheck(todo);
        _businessRules.UserCanAccessTodo(todo, userId, userRole); 

        await _todoRepository.RemoveAsync(todo);

        return new ReturnModel<string>
        {
            Data = $"Todo Başlığı : {todo.Title}",
            Message = Messages.TodoDeletedMessage,
            Status = 204,
            Success = true
        };
    }

    public async Task<ReturnModel<List<TodoResponseDto>>> GetAllByCategoryIdAsync(int categoryId, string userId, string userRole)
    {
        var todos = await _todoRepository.GetAllByCategoryIdAsync(categoryId);

        foreach (var todo in todos)
        {
            _businessRules.UserCanAccessTodo(todo, userId, userRole);
        }

        var responses = _mapper.Map<List<TodoResponseDto>>(todos);
        return new ReturnModel<List<TodoResponseDto>>
        {
            Data = responses,
            Message = $"Kategori Id'sine göre todo'lar listelendi : Kategori Id: {categoryId}",
            Status = 200,
            Success = true
        };
    }

    public async Task<ReturnModel<List<TodoResponseDto>>> GetAllByUserIdAsync(string userId, string requesterId, string userRole)
    {
        var todos = await _todoRepository.GetAllByUserIdAsync(userId);

        
        foreach (var todo in todos)
        {
            _businessRules.UserCanAccessTodo(todo, requesterId, userRole);
        }

        var responses = _mapper.Map<List<TodoResponseDto>>(todos);
        return new ReturnModel<List<TodoResponseDto>>
        {
            Data = responses,
            Message = $"Kullanıcı Id'sine göre todo'lar listelendi : Kullanıcı Id: {userId}",
            Status = 200,
            Success = true
        };
    }

    public async Task<ReturnModel<List<TodoResponseDto>>> GetAllByPriorityAsync(Priority priority, string userId, string userRole)
    {
        var todos = await _todoRepository.GetAllByPriorityAsync(priority);
        foreach (var todo in todos)
        {
            _businessRules.UserCanAccessTodo(todo, userId, userRole);
        }

        var responses = _mapper.Map<List<TodoResponseDto>>(todos);
        return new ReturnModel<List<TodoResponseDto>>
        {
            Data = responses,
            Message = $"Öncelik'e göre todo'lar listelendi : Öncelik: {priority}",
            Status = 200,
            Success = true
        };
    }

    public async Task<ReturnModel<List<TodoResponseDto>>> GetAllByDateRangeAsync(DateTime startDate, DateTime endDate, string userId, string userRole)
    {
        var todos = await _todoRepository.GetAllByDateRangeAsync(startDate, endDate);

        
        foreach (var todo in todos)
        {
            _businessRules.UserCanAccessTodo(todo, userId, userRole);
        }

        var responses = _mapper.Map<List<TodoResponseDto>>(todos);
        return new ReturnModel<List<TodoResponseDto>>
        {
            Data = responses,
            Message = $"Tarih aralığına göre todo'lar listelendi : Başlangıç Tarihi: {startDate}, Bitiş Tarihi: {endDate}",
            Status = 200,
            Success = true
        };
    }
}