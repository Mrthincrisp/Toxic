using AutoMapper;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;
using Microsoft.EntityFrameworkCore;

namespace Toxic.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public readonly ToxicDbContext _context;

        public CategoryRepository(ToxicDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategoryAsync(IMapper mapper, UpsertCategoryDTO createCategory)
        {
            var category = mapper.Map<Category>(createCategory);

            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (DbUpdateException) 
            {
                return null;
            }
        }

        public async Task<Category> DeleteCategoryAsync(int id)
        {
            var categoryToDelete = await _context.Categories.FindAsync(id);

            _context.Categories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();
            return categoryToDelete;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var singleCategory = await _context.Categories
                .SingleOrDefaultAsync(c => c.Id == id);

            return singleCategory;
        }

        public async Task<Category> UpdateCategoryAsync(int id, IMapper mapper, UpsertCategoryDTO updateDTO)
        {
            var categoryToUpdate = await _context.Categories.FindAsync(id);

            mapper.Map(updateDTO, categoryToUpdate);
            try
            {
                await _context.SaveChangesAsync();
                return categoryToUpdate;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }
    }
}
