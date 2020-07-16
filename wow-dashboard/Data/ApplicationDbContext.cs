using Microsoft.EntityFrameworkCore;
using wow_dashboard.Models;

namespace wow_dashboard.Data
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
                .HasMany(u => u.Characters)
                .WithOne(c => c.User);
            
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User);

            modelBuilder.Entity<TaskCharacter>()
                .HasKey(tc => new { tc.CharacterId, tc.TaskId });

            modelBuilder.Entity<TaskCharacter>()
                .HasOne(tc => tc.Character)
                .WithMany(c => c.TaskCharacters)
                .HasForeignKey(tc => tc.CharacterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TaskCharacter>()
                .HasOne(tc => tc.Task)
                .WithMany(t => t.TaskCharacters)
                .HasForeignKey(tc => tc.TaskId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}