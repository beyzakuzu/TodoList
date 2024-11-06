using AutoMapper;
using Core.Exceptions;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Xml.Linq;
using TodoList.DataAccess.Abstracts;
using TodoList.Modelss.Dtos.Todos.Requests;
using TodoList.Modelss.Dtos.Todos.Responses;
using TodoList.Modelss.Entities;
using TodoList.Service.Absracts;
using TodoList.Service.Constants;
using TodoList.Service.Rules;

namespace TodoList.Service.Concretes;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;
    private readonly TodoBusinessRules _businessRules;

    public TodoService(ITodoRepository repository, IMapper mapper, TodoBusinessRules businessRules)
    {
        _repository = repository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public async Task<ReturnModel<NoData>> AddAsync(CreateTodoRequest dto)
    {
        Todo todo = _mapper.Map<Todo>(dto);
        todo.Id = Guid.NewGuid();
        todo.CreatedDate = DateTime.Now;

        _businessRules.ValidateStartDate(todo);

        await _repository.AddAsync(todo);

        return new ReturnModel<NoData>
        {
            Message = Messages.TodoAddedMessage,
            Status = 201,
            Success = true
        };
    }

    public async Task<ReturnModel<NoData>> UpdateAsync(UpdateTodoRequest dto)
    {
        var existingTodo = await _repository.GetByIdAsync(dto.Id);
        _businessRules.TodoIsNullCheck(existingTodo);

        _mapper.Map(dto, existingTodo);

        _businessRules.ValidateStartDate(existingTodo);
        _businessRules.ValidatePriority(existingTodo, dto.Priority);

        await _repository.UpdateAsync(existingTodo);

        return new ReturnModel<NoData>
        {
            Message = Messages.TodoUpdatedMessage,
            Status = 200,
            Success = true
        };
    }

    public async Task<ReturnModel<NoData>> DeleteAsync(Guid id)
    {
        var todo = await _repository.GetByIdAsync(id);
        _businessRules.TodoIsNullCheck(todo);

        await _repository.RemoveAsync(todo);

        return new ReturnModel<NoData>
        {
            Message = Messages.TodoDeletedMessage,
            Status = 200,
            Success = true
        };
    }

    public async Task<ReturnModel<TodoResponseDto>> GetByIdAsync(Guid id)
    {
        var todo = await _repository.GetByIdAsync(id);
        _businessRules.TodoIsNullCheck(todo);

        var response = _mapper.Map<TodoResponseDto>(todo);

        return new ReturnModel<TodoResponseDto>
        {
            Data = response,
            Status = 200,
            Success = true,
            Message= "Id ye göre getirildi."
        };
    }

    public async Task<ReturnModel<List<TodoResponseDto>>> GetAllByPriorityAsync(Priority priority)
    {
        var todos = await _repository.GetAllAsync(t => t.Priority == priority);
        var responses = _mapper.Map<List<TodoResponseDto>>(todos);

        return new ReturnModel<List<TodoResponseDto>>
        {
            Data = responses,
            Status = 200,
            Success = true,
            Message= "Önceliğe göre görevler getirildi."
        };
    }

    public async Task<ReturnModel<List<TodoResponseDto>>> GetAllByStartDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var todos = await _repository.GetAllAsync(t => t.StartDate >= startDate && t.StartDate <= endDate);
        var responses = _mapper.Map<List<TodoResponseDto>>(todos);

        return new ReturnModel<List<TodoResponseDto>>
        {
            Data = responses,
            Status= 200,
            Success = true,
            Message= "İlgili tarih aralığınaki görevler getirildi."
        };
    }

    public async Task<ReturnModel<List<TodoResponseDto>>> GetAllByUserIdAsync(string userId)
    {
        var todos = await _repository.GetAllAsync(t => t.UserId == userId); 
        var responses = _mapper.Map<List<TodoResponseDto>>(todos); 

        return new ReturnModel<List<TodoResponseDto>>
        {
            Data = responses,
            Status = 200,
            Success = true,
            Message = "İlgili id kayıtları getirildi"
        };
    }

    public async Task<ReturnModel<NoData>> DeleteExpiredTodosAsync()
    {
        
        var currentDate = DateTime.Now;

        
        var expiredTodos = await _repository.GetAllAsync(t => t.EndDate < currentDate);

        if (!expiredTodos.Any())
        {
            return new ReturnModel<NoData>
            {
                Message = "Tarihi geçmiş yapılacak bulunamadı",
                Status = 404,
                Success = false
            };
        }

   
        foreach (var todo in expiredTodos)
        {
            await _repository.RemoveAsync(todo);
        }

        return new ReturnModel<NoData>
        {
            Message = $"{expiredTodos.Count} tarihi geçmiş görevler silindi.",
            Status = 200,
            Success = true
        };
    }
}
