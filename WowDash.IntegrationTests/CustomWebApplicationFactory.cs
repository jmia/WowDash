using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WowDash.Infrastructure;
using WowDash.WebUI;

namespace WowDash.IntegrationTests
{
    /// <summary>
    /// Custom system-under-test bootstrap class for creating a test server
    /// with a real database (instead of in-memory) that tracks the code-first
    /// migrations of my actual database. 
    /// </summary>
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Haven't figured out how to get configuration in here to store the connection string
            var connectionString = 
                "Server=(LocalDB)\\MSSQLLocalDB;Database=WowDash-Testing;Trusted_Connection=True;MultipleActiveResultSets=true";
            
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>((options, context) =>
                {
                    context.UseSqlServer(connectionString);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory>>();

                    db.Database.Migrate();
                }
            });
        }
    }
}
