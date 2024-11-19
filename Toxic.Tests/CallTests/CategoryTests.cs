using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AutoMapper;
using Toxic.Mapper;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;
using Toxic.Services;

namespace Toxic.Tests.CallTests
{
    public class CategoryTests
    {
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryTests()
        {
            _mockCategoryRepository = new Mock<ICategoryRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            _categoryService = new CategoryService(_mockCategoryRepository.Object);
        }

        [Fact] //get all categories
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

        [Fact] //Get single category
        public async Task SingleCategoryTest_WhenCalled_ReturnSingleCategory()
        {
            var category = new Category
            {
                Id = 1
            };

            _mockCategoryRepository.Setup(repo => repo.GetCategoryByIdAsync(category.Id)).ReturnsAsync(category);

            var results = await _categoryService.GetCategoryByIdAsync(category.Id);

            Assert.NotNull(results);
            Assert.Equal(category.Id, results.Id);
        }

        [Fact] // Delete category
        public async Task DeleteACategory_WhenCalled_ReturnsDeletedCategoryAsync()
        {
            var category = new Category
            {
                Id = 1,
                Title = "test"
            };

            _mockCategoryRepository.Setup(x => x.GetCategoryByIdAsync(category.Id)).ReturnsAsync(category);

            await _categoryService.DeleteCategoryAsync(category.Id);

            _mockCategoryRepository.Verify(x => x.DeleteCategoryAsync(category.Id), Times.Once);

            _mockCategoryRepository.Setup(x => x.GetCategoryByIdAsync(category.Id)).ReturnsAsync((Category)null);
        }

        [Fact] //Create category
        public async Task CreateCategoryAsync_WhenCalled_ReturnsnewCateGoryWhenCalled()
        {

            var categoryDTO = new UpsertCategoryDTO
            {
                Title = "Test",
            };

            var category = _mapper.Map<Category>(categoryDTO);

            _mockCategoryRepository.Setup(x => x.CreateCategoryAsync(_mapper, categoryDTO)).ReturnsAsync(category);

            var result = await _categoryService.CreateCategoryAsync(_mapper, categoryDTO);

            Assert.NotNull(result);
            Assert.Equal(categoryDTO.Title, result.Title);


        }

        [Fact] //Update category
        public async Task UpdateCategoryAsync_WhenCalled_ReturnUpdatedCategory()
        {
            int id = 1;

            var categoryDTO = new UpsertCategoryDTO
            {
                Title = "update me"
            };

            var category = _mapper.Map<Category>(categoryDTO);

            _mockCategoryRepository.Setup(x => x.GetCategoryByIdAsync(id)).ReturnsAsync(category);
            _mockCategoryRepository.Setup(x => x.UpdateCategoryAsync(id, _mapper, categoryDTO)).ReturnsAsync(category);

            var result = await _categoryService.UpdateCategoryAsync(id, _mapper, categoryDTO);

            Assert.NotNull(result);
            Assert.Equal(categoryDTO.Title, result.Title);

        }
    }
}
