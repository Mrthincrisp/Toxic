using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using Toxic.DTOs;
using Toxic.Interfaces;
using Toxic.Models;

namespace Toxic.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder route)
        {
            var group = route.MapGroup("/api/user/").WithTags(nameof(User));

            // Check user
            group.MapGet("/check/{id}", async (IUserService user, int id) =>
            {
                var userChecked = await user.UserCheckAsync(id);

                if (userChecked == null)
                {
                    return Results.NotFound($"User with ID {id} not found.");
                }

                return Results.Ok(userChecked);
            }).WithName("Check User")
                .WithOpenApi()
                .Produces<User>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            // Login
            group.MapGet("/login/{uid}", async (IUserService user, string uid) =>
            {
                var userLogin = await user.UserLoginAsync(uid);

                if (userLogin == null)
                {
                    return Results.NotFound($"User with UID {uid} not found.");
                }
                return Results.Ok(userLogin);
            }).WithName("User Login")
                .WithOpenApi()
                .Produces<User>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            // Single user
            group.MapGet("/{id}", async (IUserService user, int id) =>
            {

                var singleUser = await user.GetASingleUserAsync(id);

                if (singleUser == null)
                {
                    return Results.NotFound($"User with UID {id} not found.");
                }
                return Results.Ok(singleUser);

            }).WithName("get a single User")
                .WithOpenApi()
                .Produces<User>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound);

            // Delete user
            group.MapDelete("/{id}", async (IUserService user, int id) => 
            {

                var deletedUser = await user.DeleteUserAsync(id);

                if (deletedUser == null)
                {
                    return Results.NotFound($"User with UID {id} not found.");
                }

                return Results.Ok("user, and all content related deleted.");

            });

            // Register
            group.MapPost("/create", async (IUserService user, IMapper mapper, CreateUserDTO createDTO) => 
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(createDTO);
                bool isValid = Validator.TryValidateObject(createDTO, validationContext, validationResults, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
                }

                var createdUser = await user.RegisterUserAsync(mapper, createDTO);

                if (createdUser == null)
                {
                    return Results.BadRequest("Unable to create User");
                }


                return Results.Created($"/api/category/{createdUser.Id}", createdUser);

            }).WithName("Create User")
            .WithOpenApi()
            .Produces<Category>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

            // Update user
            group.MapPut("/{id}", async (IUserService user, int id, IMapper mapper, CreateUserDTO updateDTO) =>
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(updateDTO);
                bool isValid = Validator.TryValidateObject(updateDTO, validationContext, validationResults, true);

                if (!isValid)
                {
                    return Results.BadRequest(validationResults.Select(v => v.ErrorMessage));
                }

                var updatedUser = await user.RegisterUserAsync(mapper, updateDTO);

                if (updatedUser == null)
                {
                    return Results.BadRequest("Unable to create User");
                }


                return Results.Created($"/api/category/{updatedUser.Id}", updatedUser);

            }).WithName("Update User")
            .WithOpenApi()
            .Produces<Category>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        }
    }
}
