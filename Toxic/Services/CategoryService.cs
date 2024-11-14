using AutoMapper;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo) 
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<Category> CreateCategoryAsync(IMapper mapper, UpsertCategoryDTO createCategory)
        {
            return await _categoryRepo.CreateCategoryAsync(mapper, createCategory);
        }

        public async Task<Category> DeleteCategoryAsync(int id)
        {
            return await _categoryRepo.DeleteCategoryAsync(id);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepo.GetAllCategoriesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepo.GetCategoryByIdAsync(id);
        }

        public async Task<Category> UpdateCategoryAsync(int id, IMapper mapper, UpsertCategoryDTO updateCategory)
        {
            return await _categoryRepo.UpdateCategoryAsync(id, mapper, updateCategory);
        }
    }
}
