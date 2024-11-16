using Toxic.Mapper;
using Microsoft.AspNetCore.Http.Json;
using Toxic.Interfaces;
using Toxic.Repository;
using Toxic.Services;
using Toxic.Endpoints;

namespace Toxic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:3000", "https://localhost:8000")
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader();
                });
            });

            // allows passing datetimes without time zone data 
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            builder.Services.AddNpgsql<ToxicDbContext>(builder.Configuration["ToxicDbConnectionString"]);

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ICommentService, CommentService>();

            builder.Services.AddScoped<IMessageRepository, MessageRepository>();
            builder.Services.AddScoped<IMessageService, MessageService>();

            builder.Services.AddScoped<ITopicRepository, TopicRepository>();
            builder.Services.AddScoped<ITopicService, TopicService>();

            var app = builder.Build();

            app.UseCors();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            
            app.MapCategoryEndpoints();
            app.MapCommentEndpoints();
            app.MapMessageEndpoints();
            app.MapTopicEdnpoints();

            app.Run();
        }
    }
}
