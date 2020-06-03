using Microsoft.EntityFrameworkCore;

namespace wow_dashboard {
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    //public DbSet<Something> Stuff { get; set; }
  }
}