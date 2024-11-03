using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DataAccess.Abstracts;
using TodoList.Modelss.Dtos.Categories.Requests;
using TodoList.Modelss.Entities;
using TodoList.Service.Concretes;

namespace Service.Tests
{
    
    public class CategoryServiceTest
    {
        private CategoryService _categoryService;
        private Mock<ICategoryRepository> _categoryRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object);
        }

        [Test]
        public async Task AddAsync_WhenCategoryIsAdded_ReturnsSuccess()
        {
            // Arrange
            var dto = new CreateCategoryRequest("Test Category");
            var category = new Category { Id = 1, Name = dto.Name };
            _categoryRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Category>())).ReturnsAsync(category);

            // Act
            var result = await _categoryService.AddAsync(dto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(category.Name, result.Data.Name);
            Assert.AreEqual("Kategori başarıyla eklendi.", result.Message);
            Assert.AreEqual(200, result.Status);
        }

      

        [Test]
        public async Task GetByIdAsync_WhenCategoryExists_ReturnsCategory()
        {
            // Arrange
            var categoryId = 1;
            var category = new Category { Id = categoryId, Name = "Test Category" };
            _categoryRepositoryMock.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(category);

            // Act
            var result = await _categoryService.GetByIdAsync(categoryId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(category.Name, result.Data.Name);
            Assert.AreEqual("Kategori başarıyla bulundu.", result.Message);
            Assert.AreEqual(200, result.Status);
        }

        [Test]
        public async Task GetByIdAsync_WhenCategoryDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var categoryId = 99; 
            _categoryRepositoryMock.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync((Category)null);

            // Act
            var result = await _categoryService.GetByIdAsync(categoryId);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.AreEqual("Kategori bulunamadı.", result.Message);
            Assert.AreEqual(404, result.Status);
        }

        [Test]
        public async Task UpdateAsync_WhenCategoryIsUpdated_ReturnsSuccess()
        {
            // Arrange
            var dto = new UpdateCategoryRequest(1, "Updated Category");
            var existingCategory = new Category { Id = 1, Name = "Old Category" };
            var updatedCategory = new Category { Id = 1, Name = dto.Name };

            _categoryRepositoryMock.Setup(x => x.GetByIdAsync(dto.Id)).ReturnsAsync(existingCategory);
            _categoryRepositoryMock.Setup(x => x.UpdateAsync(existingCategory)).ReturnsAsync(updatedCategory);

            // Act
            var result = await _categoryService.UpdateAsync(dto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(updatedCategory.Name, result.Data.Name);
            Assert.AreEqual("Kategori başarıyla güncellendi.", result.Message);
            Assert.AreEqual(200, result.Status);
        }

        [Test]
        public async Task DeleteAsync_WhenCategoryIsDeleted_ReturnsSuccess()
        {
            // Arrange
            var categoryId = 1; 
            var existingCategory = new Category { Id = categoryId, Name = "Test Category" };

            _categoryRepositoryMock.Setup(x => x.GetByIdAsync(categoryId)).ReturnsAsync(existingCategory);
            _categoryRepositoryMock.Setup(x => x.RemoveAsync(existingCategory)).ReturnsAsync(existingCategory); 

            // Act
            var result = await _categoryService.DeleteAsync(categoryId);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Data);
            Assert.AreEqual("Kategori başarıyla silindi.", result.Message);
            Assert.AreEqual(200, result.Status);
        }

    }
}
