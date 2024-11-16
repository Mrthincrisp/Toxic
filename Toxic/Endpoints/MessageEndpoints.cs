using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Endpoints
{
    public static class MessageEndpoints
    {
        public static void MapMessageEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("api/message").WithTags(nameof(Message));


            //Create message
            group.MapPost("/new", async (IMessageService message, IMapper mapper, CreateMessageDTO createDTO) =>
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(createDTO);
                bool isValid = Validator.TryValidateObject(createDTO, validationContext, validationResults, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
                }

                var createdMessage = await message.CreateAMessage(mapper, createDTO);

                if (createdMessage == null)
                {
                    return Results.BadRequest("Unable to create message");
                }

                return Results.Created($"/api/message/{createdMessage.Id}", createdMessage);
            })
                .WithName("Create Message")
                .WithOpenApi()
                .Produces<Category>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest);

            //Update message
            group.MapPut("/{id}", async (IMessageService message, int id, IMapper mapper, UpdateMessageDTO updateDTO) =>
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(updateDTO);
                bool isValid = Validator.TryValidateObject(updateDTO, validationContext, validationResults, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
                }

                var updatedMessage = await message.UpdateMessageAsync(id, mapper, updateDTO);

                if (updatedMessage == null)
                {
                    return Results.BadRequest("Unable to create message");
                }

                return Results.Created($"/api/message/{updatedMessage.Id}", updatedMessage);
            })
                .WithName("Update Message")
                .WithOpenApi()
                .Produces<Category>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest);

            //Delete message
            group.MapDelete("/{id}", async (IMessageService message, int id) =>
            {
                var deletedMessage = await message.DeleteMessageAsync(id);

                if (deletedMessage == null)
                {
                    return Results.NotFound($"Message id, {id} not found");
                }

                return Results.Ok("message deleted");
            })
            .WithName("Delete a Message")
            .WithOpenApi()
            .Produces(StatusCodes.Status404NotFound)
            .Produces<Category>(StatusCodes.Status204NoContent);

            //Get single message
            group.MapGet("/{id}", async (IMessageService message, int id) =>
            {

                var singleMessage = await message.GetSingleMessageAsync(id);

                if (singleMessage == null)
                {
                    return Results.NotFound($"message id {id} not found");
                }

                return Results.Ok(singleMessage);

            }).WithName("Get a Message.")
              .WithOpenApi()
              .Produces(StatusCodes.Status404NotFound)
              .Produces<Category>(StatusCodes.Status200OK);

            //Get chat messages
            group.MapGet("/{id}/chat", async (IMessageService message, int id) =>
            {

                var chatMessages = await message.GetMessagesInAChatAsync(id);

                if (chatMessages == null)
                {
                    return Results.NotFound($"messages for id {id} not found.");
                }

                return Results.Ok(chatMessages);

            })
              .WithName("chat messages")
              .WithOpenApi()
              .Produces(StatusCodes.Status404NotFound)
              .Produces<Category>(StatusCodes.Status200OK);

        }
    }
}
