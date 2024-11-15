using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Endpoints
{
    public static class CommentEndpoints
    {
        public static void MapCommentEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("api/comment").WithTags(nameof(Comment));

            //Create comment
            group.MapPost("/new", async(ICommentService comment, IMapper mapper, CreateCommentDTO createDTO) =>
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(createDTO);
                bool isValid = Validator.TryValidateObject(createDTO, validationContext, validationResults, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
                }

                var createdComment = await comment.CreateCommentAsync(mapper, createDTO);

                if (createdComment == null)
                {
                    return Results.BadRequest("Unable to create comment.");
                }

                return Results.Created($"/api/comment/{createdComment.Id}", createdComment);

            })
              .WithName("Create comment")
              .WithOpenApi()
              .Produces<Comment>(StatusCodes.Status201Created)
              .Produces(StatusCodes.Status400BadRequest);

            //Update comment
            group.MapPut("/{id}", async(ICommentService comment, int id, IMapper mapper, UpdateCommentDTO updateDTO) =>
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(updateDTO);
                bool isValid = Validator.TryValidateObject(updateDTO, validationContext, validationResults, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
                }

                var updatedComment = await comment.UpdateCommentAsync(id, mapper, updateDTO);

                if (updatedComment == null)
                {
                    return Results.BadRequest("Failed to update Category");
                }

                return Results.Created($"api/comment/{updatedComment.Id}", updatedComment);
            })
              .WithName("Update comment")
              .WithOpenApi()
              .Produces<Comment>(StatusCodes.Status201Created)
              .Produces(StatusCodes.Status400BadRequest);

            //Get topic comments
            group.MapGet("/topic/{id}", async(ICommentService comment, int id) =>
            {
                var topicComment = await comment.GetCommentsInTopicAsync(id);

                if (topicComment == null)
                {
                    return Results.NotFound($"no topic with id, {id} found.");
                }

                return Results.Ok(topicComment);
            })
              .WithName("Topic comments")
              .WithOpenApi()
              .Produces(StatusCodes.Status404NotFound)
              .Produces<Comment>(StatusCodes.Status200OK);

            //Get a single comment
            group.MapGet("/{id}", async (ICommentService comment, int id) =>
            {
                var singleComment = await comment.GetACommentAsync(id);

                if (singleComment == null)
                {
                    return Results.NotFound($"No domment found with id, {id}");
                }

                return Results.Ok(singleComment);
            })
              .WithDisplayName("Single comment")
              .WithOpenApi()
              .Produces(StatusCodes.Status404NotFound)
              .Produces<Comment>(StatusCodes.Status200OK);

            //Delete a comment
            group.MapDelete("/{id}", async(ICommentService comment, int id) =>
            {
                var deltedComment = await comment.DeleteACommentAsync(id);

                if (deltedComment == null)
                {
                    return Results.NotFound($"No comment with id, {id} found");
                }
                return Results.Ok("Comment deleted");
            })
              .WithDescription("Deletes a comment")
              .WithOpenApi()
              .Produces(StatusCodes.Status404NotFound)
              .Produces(StatusCodes.Status200OK);
        }
    }
}
