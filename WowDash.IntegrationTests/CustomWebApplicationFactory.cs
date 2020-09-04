using System;
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
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Haven't figured out how to get configuration in here to store the connection string
            var connectionString = "Server=(LocalDB)\\MSSQLLocalDB;Database=WowDash-Testing;Trusted_Connection=True;MultipleActiveResultSets=true";
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

                    try
                    {
                        // These are wiped before the first test run, so they need a better home
                        //var defaultUser = new Player { DisplayName = "Jen", DefaultRealm = "area-52" };

                        //db.Players.Add(defaultUser);

                        //var scully = new Character
                        //{
                        //    Name = "Scully",
                        //    Gender = CharacterGender.Female,
                        //    Realm = "area-52",
                        //    Class = "Hunter",
                        //    Race = "Blood Elf",
                        //    Level = 120,
                        //    PlayerId = defaultUser.Id
                        //};

                        //var chakwas = new Character
                        //{
                        //    Name = "Chakwas",
                        //    Gender = CharacterGender.Female,
                        //    Realm = "area-52",
                        //    Class = "Druid",
                        //    Race = "Highmountain Tauren",
                        //    Level = 120,
                        //    PlayerId = defaultUser.Id
                        //};

                        //db.Characters.AddRange(scully, chakwas);

                        //db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
}
