using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Toxic.Interfaces;
using Toxic.Models;
using Toxic.Services;

namespace Toxic.Tests.CallTests
{
    public class CategoryTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly ICategoryService _categoryService;

        public CategoryTests()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_mockCategoryRepository.Object);
        }

        [Fact] 
        public async Task GetCategoriesAsync_WhenCalled_ReturnCategoriesAsync()
        {
            var categories = new List<Category>
            {
                new Category {Id = 1 },
                new Category {Id = 2 },
                new Category {Id = 3 }
            };

            _mockCategoryRepository.Setup(x => x.GetAllCategoriesAsync()).ReturnsAsync(categories);

            var  results = await _categoryService.GetAllCategoriesAsync();

            Assert.NotNull(results);
            Assert.Equal(3, results.Count);
        }

        [Fact]
        public async Task SingleCategoryTest()
        {
            var category = new Category
            {
                Id = 1
            };

            _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(category.Id));

            var results = await _categoryService.GetCategoryByIdAsync(category.Id);

            Assert.NotNull(results);
            Assert.Equal(category.Id, results.Id);
        }
    }
}
