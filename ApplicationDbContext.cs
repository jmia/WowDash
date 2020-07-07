using Microsoft.EntityFrameworkCore;
using wow_dashboard.Models;

namespace wow_dashboard
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskCharacter> TaskCharacters { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.DefaultCharacter)
                .WithOne(c => c.User);
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Characters)
                .WithOne(c => c.User);
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User);

            modelBuilder.Entity<Character>()
                .HasMany(c => c.TaskCharacters)
                .WithOne(tc => tc.Character);

            modelBuilder.Entity<Task>()
                .HasMany(t => t.TaskCharacters)
                .WithOne(tc => tc.Task);

            // TODO - Configure "owned" entity types?
        }
    }
}