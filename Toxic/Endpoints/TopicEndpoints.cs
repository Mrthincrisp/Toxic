using Toxic.Interfaces;
using Toxic.Models;
using AutoMapper;
using Toxic.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Toxic.Endpoints
{
    public static class TopicEndpoints
    {
        public static void MapTopicEdnpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/topic").WithTags(nameof(Category));

            //Get topics of a category
            group.MapGet("/category/{id}", async (ITopicService topic, int id) =>
            {
                var categoryTopics = await topic.GetAllCategoryTopicsAsync(id);

                if (categoryTopics == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(categoryTopics);

            })
                .WithName("Category Topics")
                .WithOpenApi()
                .Produces<List<Topic>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            //Get a Single topic
            group.MapGet("/{id}", async (ITopicService topic, int id) =>
            {
                var singleTopic = await topic.GetTopicByIdAsync(id);

                if (singleTopic == null)
                {
                    return Results.NotFound($"No topic id, {id} found.");
                }
                return Results.Ok(singleTopic);
            }).WithName("Single Topic")
            .WithOpenApi()
            .Produces(StatusCodes.Status404NotFound)
            .Produces<Topic>(StatusCodes.Status200OK);

            //Update a topic
            group.MapPut("/{id}", async (ITopicService topic, int id, IMapper mapper, UpdateTopicDTO updateDTO) =>
            {

                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(updateDTO);
                bool isValid = Validator.TryValidateObject(updateDTO, validationContext, validationResults, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
                }

                var updatedTopic = await topic.UpdateTopicAsync(id, mapper, updateDTO);

                if (id <= 0)
                {
                    return Results.BadRequest("Invalid ID.");
                }

                if (updatedTopic == null)
                {
                    return Results.BadRequest("Failed to update Category");
                }

                return Results.Created($"api/category/{updatedTopic.Id}", updateDTO);

            })
            .WithName("Update Topic")
            .WithOpenApi()
            .Produces<Category>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

            //Delete a topic
            group.MapDelete("/{id}", async (ITopicService topic, int id) =>
            {
                var deletedTopic = await topic.DeleteTopicAsync(id);

                if (deletedTopic == null)
                {
                    return Results.NotFound($"Topic id, {id} not found.");
                }

                return Results.Ok("Topic, and related comments deleted.");

            })
            .WithName("Delete a Topic")
            .WithOpenApi()
            .Produces<Category>(StatusCodes.Status204NoContent);

            //Create a topic
            group.MapPost("/new", async (ITopicService topic, IMapper mapper, CreateTopicDTO createDTO) =>
            {

                var createdTopic = await topic.CreateTopicAsync(mapper, createDTO);
                if (createdTopic == null)
                {
                    return Results.BadRequest("Unable to create Category");
                }


                return Results.Created($"/api/category/{createdTopic.Id}", createdTopic);
            }).WithName("Create Topic")
            .WithOpenApi()
            .Produces<Category>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}
