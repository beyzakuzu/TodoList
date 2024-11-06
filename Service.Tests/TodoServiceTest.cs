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
using TodoList.Service.Constants;
using Core.Responses;

namespace Service.Tests
{
    [TestFixture]
    public class TodoServiceTest
    {
        private TodoService _todoService;
        private Mock<ITodoRepository> _repositoryMock;
        private Mock<IMapper> _mockMapper;
        private Mock<TodoBusinessRules> _rulesMock;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<ITodoRepository>();
            _mockMapper = new Mock<IMapper>();
            _rulesMock = new Mock<TodoBusinessRules>();
            _todoService = new TodoService(_repositoryMock.Object, _mockMapper.Object, _rulesMock.Object);
        }

        [Test]
        public async Task Add_WhenTodoAdded_ReturnsSuccess()
        {
            // Arrange
            var dto = new CreateTodoRequest("Test Todo", "Description", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), Priority.High, 1);
            var todo = new Todo
            {
                Id = Guid.NewGuid(),
                Title = "Test Todo",
                Description = "Description",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                Priority = Priority.High,
                CategoryId = 1,
                CreatedDate = DateTime.Now
            };
            var response = new TodoResponseDto(
                todo.Id,
                todo.Title,
                todo.Description,
                todo.StartDate,
                todo.EndDate,
                todo.CreatedDate,
                todo.Priority,
                todo.CategoryId,
                false, // Completed
                "Test User",
                "userId",
                "General"
            );

            _mockMapper.Setup(x => x.Map<Todo>(dto)).Returns(todo);
            _mockMapper.Setup(x => x.Map<TodoResponseDto>(It.IsAny<Todo>())).Returns(response);
            _mockMapper.Setup(x => x.Map<TodoResponseDto>(todo)).Returns(response);

            // Act
            var result = await _todoService.AddAsync(dto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(201, result.Status);
            Assert.AreEqual(Messages.TodoAddedMessage, result.Message);
        }

        [Test]
        public async Task Update_WhenTodoUpdated_ReturnsSuccess()
        {
            // Arrange
            var dto = new UpdateTodoRequest(
                Guid.NewGuid(),
                "Updated Todo",
                "Updated Description",
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                Priority.Normal,
                2,
                false);
            var existingTodo = new Todo
            {
                Id = dto.Id,
                Title = "Old Todo",
                Description = "Old Description",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                Priority = Priority.Low,
                CategoryId = 1,
                CreatedDate = DateTime.Now
            };
            var updatedTodo = new Todo
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Priority = dto.Priority,
                CategoryId = dto.CategoryId,
                CreatedDate = existingTodo.CreatedDate
            };
            var response = new TodoResponseDto(
                updatedTodo.Id,
                updatedTodo.Title,
                updatedTodo.Description,
                updatedTodo.StartDate,
                updatedTodo.EndDate,
                updatedTodo.CreatedDate,
                updatedTodo.Priority,
                updatedTodo.CategoryId,
                false, // Completed
                "Test User",
                "userId",
                "General"
            );

            _repositoryMock.Setup(x => x.GetByIdAsync(dto.Id)).ReturnsAsync(existingTodo);
            _mockMapper.Setup(x => x.Map(dto, existingTodo));
            _mockMapper.Setup(x => x.Map<TodoResponseDto>(existingTodo)).Returns(response);

            // Act
            var result = await _todoService.UpdateAsync(dto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(200, result.Status);
            Assert.AreEqual(Messages.TodoUpdatedMessage, result.Message);
        }

        [Test]
        public async Task GetById_WhenTodoNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var todoId = Guid.NewGuid();
            Todo todo = null;
            _rulesMock.Setup(x => x.TodoIsNullCheck(todo)).Throws(new NotFoundException("Todo not found"));

            // Assert
            var exception = Assert.ThrowsAsync<NotFoundException>(() => _todoService.GetByIdAsync(todoId));
            Assert.AreEqual("Todo not found", exception.Message);
        }

        [Test]
        public async Task GetById_WhenTodoFound_ReturnsSuccess()
        {
            // Arrange
            var todoId = Guid.NewGuid();
            var todo = new Todo
            {
                Id = todoId,
                Title = "Test Todo",
                Description = "Description",
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2),
                Priority = Priority.Normal,
                CategoryId = 1,
                CreatedDate = DateTime.Now
            };
            var response = new TodoResponseDto(
                todo.Id,
                todo.Title,
                todo.Description,
                todo.StartDate,
                todo.EndDate,
                todo.CreatedDate,
                todo.Priority,
                todo.CategoryId,
                false, // Completed
                "Test User",
                "userId",
                "General"
            );

            _repositoryMock.Setup(x => x.GetByIdAsync(todoId)).ReturnsAsync(todo);
            _rulesMock.Setup(x => x.TodoIsNullCheck(todo));
            _mockMapper.Setup(x => x.Map<TodoResponseDto>(todo)).Returns(response);

            // Act
            var result = await _todoService.GetByIdAsync(todoId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(200, result.Status);
            Assert.AreEqual(response, result.Data);
        }
    }
}