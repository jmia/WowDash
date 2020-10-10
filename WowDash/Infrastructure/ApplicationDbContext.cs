using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WowDash.ApplicationCore.Entities;

namespace WowDash.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options) { }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskCharacter> TaskCharacters { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Player>()
                .HasMany(u => u.Characters)
                .WithOne(c => c.Player);

            modelBuilder.Entity<Player>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.Player);

            modelBuilder.Entity<Task>().OwnsMany(t => t.GameDataReferences);

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