using Toxic.Models;
using Toxic.Interfaces;
using Toxic.DTOs;
using AutoMapper;


namespace Toxic.Endpoints
{
    public static class ChatEndpoints
    {
        public static void MapChatEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/chat").WithTags(nameof(Chat));

            //Create Chat with users
            group.MapPost("new/{id}", async (IChatService chat, IMapper mapper, int id, List<int> userIds) =>
            {
                var createdChat = await chat.CreateChatWithUsersAsync(mapper, id, userIds);

                return Results.Created($"api/chat/new/{createdChat.Id}", createdChat);
            })
                .WithName("created chat")
                .WithOpenApi()
                .Produces(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status404NotFound);

            //Single chat
            group.MapGet("/{id}", async (IChatService chat, int id) =>
            {

                var singleChat = await chat.GetChatByIdAsync(id);

                if (singleChat == null)
                {
                    return Results.NotFound("no chat found");
                }

                return Results.Ok(singleChat);

            })
                .WithName("single chat")
                .WithOpenApi()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            //User chats
            group.MapGet("/user/{userId}", async (IChatService chat, int userId) =>
            {

                var userChats = await chat.GetUserChats(userId);

                return Results.Ok(userChats);

            })
                .WithName("user's chat")
                .WithOpenApi()
                .Produces(StatusCodes.Status200OK);
        }
    }
}
