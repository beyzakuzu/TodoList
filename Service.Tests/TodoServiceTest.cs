using Moq;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DataAccess.Abstracts;
using TodoList.Service.Concretes;
using TodoList.Service.Rules;
using TodoList.Modelss.Dtos.Todos.Requests;
using TodoList.Modelss.Entities;
using TodoList.Modelss.Dtos.Todos.Responses;
using Core.Exceptions;
using System.Linq.Expressions;

namespace Service.Tests
{
    public class TodoServiceTest
    {
        private TodoService _todoService;
        private Mock<ITodoRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<TodoBusinessRules> _mockBusinessRules;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<ITodoRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockBusinessRules = new Mock<TodoBusinessRules>();
            _todoService = new TodoService(_mockRepository.Object, _mockMapper.Object, _mockBusinessRules.Object);
        }

        [Test]
        public async Task AddAsync_WhenTodoAdded_ReturnsSuccess()
        {
            // Arrange
            CreateTodoRequest dto = new CreateTodoRequest("Test Todo", "Description", DateTime.Now, DateTime.Now.AddDays(1), Priority.Normal, 1);
            Todo createdTodo = new Todo
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Priority = dto.Priority,
                CategoryId = dto.CategoryId,
                UserId = "user123"
            };
            TodoResponseDto responseDto = new TodoResponseDto(
                createdTodo.Id,
                createdTodo.Title,
                createdTodo.Description,
                createdTodo.StartDate,
                createdTodo.EndDate,
                DateTime.Now,
                createdTodo.Priority,
                createdTodo.CategoryId,
                false,
                "TestUser",
                createdTodo.UserId,
                "TestCategory"
            );

            _mockMapper.Setup(m => m.Map<Todo>(dto)).Returns(createdTodo);
            _mockRepository.Setup(r => r.AddAsync(createdTodo)).ReturnsAsync(createdTodo);
            _mockMapper.Setup(m => m.Map<TodoResponseDto>(createdTodo)).Returns(responseDto);

            // Act
            var result = await _todoService.AddAsync(dto, "user123");

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(responseDto, result.Data);
            Assert.AreEqual(201, result.Status);
        }

        [Test]
        public async Task UpdateAsync_WhenTodoUpdated_ReturnsSuccess()
        {
            // Arrange
            UpdateTodoRequest dto = new UpdateTodoRequest(Guid.NewGuid(), "Updated Todo", "Updated Description", DateTime.Now, DateTime.Now.AddDays(2), Priority.Low, 1, true);
            var todo = new Todo
            {
                Id = dto.Id,
                Title = "Old Todo",
                Description = "Old Description",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                Priority = Priority.Normal,
                CategoryId = 1,
                Completed = false,
                UserId = "user123"
            };
            var responseDto = new TodoResponseDto(
                dto.Id,
                dto.Title,
                dto.Description,
                dto.StartDate,
                dto.EndDate,
                DateTime.Now,
                dto.Priority,
                dto.CategoryId,
                dto.Completed,
                "TestUser",
                todo.UserId,
                "TestCategory"
            );

            _mockRepository.Setup(r => r.GetByIdAsync(dto.Id)).ReturnsAsync(todo);
            _mockMapper.Setup(m => m.Map<Todo>(dto)).Returns(todo);
            _mockRepository.Setup(r => r.UpdateAsync(todo)).ReturnsAsync(todo);
            _mockMapper.Setup(m => m.Map<TodoResponseDto>(todo)).Returns(responseDto);

            // Act
            var result = await _todoService.UpdateAsync(dto, "user123", "UserRole");

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(responseDto, result.Data);
            Assert.AreEqual(200, result.Status);
        }

        
    }
}
