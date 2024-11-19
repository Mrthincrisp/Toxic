using AutoMapper;
using Toxic.DTOs;
using Toxic.Models;
namespace Toxic.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> UpdateCategoryAsync(int id, IMapper mapper, UpsertCategoryDTO updateCategory);
        Task<Category> DeleteCategoryAsync(int id);
        Task<Category> CreateCategoryAsync(IMapper mapper, UpsertCategoryDTO createCategory);
    }
}