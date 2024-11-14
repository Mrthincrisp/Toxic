using Microsoft.EntityFrameworkCore;
using Toxic.Data;
using Toxic.Models;

namespace Toxic
{
    public class ToxicDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Topic>? Topics { get; set; }
        public DbSet<Message>? Messages { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Chat>? Chats { get; set; }
        public DbSet<Category>? Categories { get; set; }

        public ToxicDbContext(DbContextOptions<ToxicDbContext> context) : base(context) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>().HasData(UserData.Users);
            modelBuilder.Entity<Topic>().HasData(TopicData.Topics);
            modelBuilder.Entity<Message>().HasData(MessageData.Messages);
            modelBuilder.Entity<Comment>().HasData(CommentData.Comments);
            modelBuilder.Entity<Category>().HasData(CategoryData.Categories);
            modelBuilder.Entity<Chat>().HasData(ChatData.Chats);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Chats)
                .WithMany(c => c.Users)
                .UsingEntity(t => t.ToTable("UserChats"));

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Users)
                .WithMany(u => u.Messages)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Topic)
                .WithMany(t => t.Comments)
                .OnDelete(DeleteBehavior.Cascade);

        }


    }
}
