using Toxic.Models;
using Toxic.Interfaces;
using Toxic.DTOs;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Toxic.Endpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/category").WithTags(nameof(Category));

            //Get all Categories
            group.MapGet("/all", async (ICategoryService category) =>
            {
                var categories = await category.GetAllCategoriesAsync();

                if (categories == null || !categories.Any())
                {
                    return Results.NotFound("No Categories found.");
                }

                return Results.Ok(categories);
            })
                .WithName("Get all categories.")
                .WithOpenApi()
                .Produces<List<Category>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            //Get Single Category
            group.MapGet("/{id}", async (ICategoryService category, int id) =>
            {
                var singleCategory = await category.GetCategoryByIdAsync(id);

                if (singleCategory == null)
                {
                    return Results.NotFound($"Category id, {id} is does not exist");
                }
                return Results.Ok(singleCategory);

            }).WithName("Get a Category.")
              .WithOpenApi()
              .Produces(StatusCodes.Status404NotFound)
              .Produces<Category>(StatusCodes.Status200OK);

            //Update a Category
            group.MapPut("/{id}", async (ICategoryService category, IMapper mapper, UpsertCategoryDTO updateDTO, int id) =>
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(updateDTO);
                bool isValid = Validator.TryValidateObject(updateDTO, validationContext, validationResults, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
                }

                var categoryUpdated = await category.UpdateCategoryAsync(id, mapper, updateDTO);

                if (id <= 0)
                {
                    return Results.BadRequest("Invalid ID.");
                }

                if (categoryUpdated == null)
                {
                    return Results.BadRequest("Failed to update Category");
                }

                return Results.Created($"api/category/{categoryUpdated.Id}", updateDTO);
            })
            .WithName("Update Category")
            .WithOpenApi()
            .Produces<Category>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);


            //Delete a Category
            group.MapDelete("/{id}", async (ICategoryService category, int id) =>
            {

                var deletedCategory = await category.DeleteCategoryAsync(id);

                if (deletedCategory == null)
                {
                    return Results.NotFound($"Category id, {id} not found.");
                }

                return Results.Ok("Category, and related threads, and comments deleted.");

            }).WithName("Delete a Category")
            .WithOpenApi()
            .Produces<Category>(StatusCodes.Status204NoContent);

            //Create a Category
            group.MapPost("/new", async (ICategoryService category, IMapper mapper, UpsertCategoryDTO createDTO) =>
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(createDTO);
                bool isValid = Validator.TryValidateObject(createDTO, validationContext, validationResults, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
                }

                var createdCategory = await category.CreateCategoryAsync(mapper, createDTO);

                if(createdCategory == null)
                {
                    return Results.BadRequest("Unable to create Category");
                }


                return Results.Created($"/api/category/{createdCategory.Id}", createdCategory);
            }).WithName("Create Category")
            .WithOpenApi()
            .Produces<Category>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}
