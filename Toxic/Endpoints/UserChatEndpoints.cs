using Microsoft.AspNetCore.Mvc;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Endpoints
{
    public static class UserChatEndpoints
    {
        public static void MapUserChatEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("api/userchat").WithTags(nameof(UserChat));

            //add user to chat
            group.MapPost("/new", async (IUserChatService userChat, int userId, int chatId) =>
            {
                Console.WriteLine("hey");
                try
                {
                    var createdUserChat = await userChat.AddUserToAChatAsync(userId, chatId);

                    if (createdUserChat == null)
                    {
                        return Results.BadRequest("user, or chat not found.");
                    }
                    return Results.Created($"/api/userchat/{createdUserChat.ChatId}", createdUserChat);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }).WithName("Create UserChat")
            .WithOpenApi()
            .Produces<UserChat>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            //Delete a chat
            group.MapDelete("user/{userId}/chat/{chatId}", async (IUserChatService userChat, int userId, int chatId) =>
            {
                var deletedUserChat = await userChat.DeleteChatAsync(userId, chatId);

                if (deletedUserChat == null)
                {
                    return Results.NotFound("user not found");
                }

                return Results.Ok("User left the chat");
            })
            .WithName("Delete UserChat")
            .WithOpenApi()
            .Produces<UserChat>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        }
    }
}
