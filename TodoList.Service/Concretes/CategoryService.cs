using AutoMapper;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DataAccess.Abstracts;
using TodoList.Modelss.Dtos.Categories.Requests;
using TodoList.Modelss.Dtos.Categories.Responses;
using TodoList.Modelss.Entities;
using TodoList.Service.Absracts;
using TodoList.Service.Constants;
using TodoList.Service.Rules;

namespace TodoList.Service.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryBusinessRules _businessRules;

        public CategoryService(ICategoryRepository categoryRepository, CategoryBusinessRules businessRules)
        {
            _categoryRepository = categoryRepository;
            _businessRules = businessRules;
        }

        public async Task<ReturnModel<CategoryResponseDto>> AddAsync(CreateCategoryRequest dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Todos = new List<Todo>()
            };

            var addedCategory = await _categoryRepository.AddAsync(category);
            var response = new CategoryResponseDto(addedCategory.Id, addedCategory.Name);

            return new ReturnModel<CategoryResponseDto>
            {
                Data = response,
                Message = Messages.CategoryAddedMessage,
                Status = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<List<CategoryResponseDto>>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var response = categories.Select(c => new CategoryResponseDto(c.Id, c.Name)).ToList();

            return new ReturnModel<List<CategoryResponseDto>>
            {
                Data = response,
                Message = "Kategoriler başarıyla listelendi.",
                Status = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<CategoryResponseDto>> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            _businessRules.CategoryIsNullCheck(category);

            var response = new CategoryResponseDto(category.Id, category.Name);
            return new ReturnModel<CategoryResponseDto>
            {
                Data = response,
                Message = "Kategori başarıyla bulundu.",
                Status = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<CategoryResponseDto>> UpdateAsync(UpdateCategoryRequest dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.Id);
            _businessRules.CategoryIsNullCheck(category);

            category.Name = dto.Name;
            var updatedCategory = await _categoryRepository.UpdateAsync(category);

            var response = new CategoryResponseDto(updatedCategory.Id, updatedCategory.Name);
            return new ReturnModel<CategoryResponseDto>
            {
                Data = response,
                Message = "Kategori başarıyla güncellendi.",
                Status = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<string>> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            _businessRules.CategoryIsNullCheck(category);

            await _categoryRepository.RemoveAsync(category);
            return new ReturnModel<string>
            {
                Data = null,
                Message = "Kategori başarıyla silindi.",
                Status = 200,
                Success = true
            };
        }

        public async Task<ReturnModel<List<Todo>>> GetAllTodosByCategoryIdAsync(int categoryId)
        {
            var todos = await _categoryRepository.GetAllTodosByCategoryIdAsync(categoryId);
            return new ReturnModel<List<Todo>>
            {
                Data = todos,
                Message = "Todo'lar başarıyla listelendi.",
                Status = 200,
                Success = true
            };
        }
    }
}
