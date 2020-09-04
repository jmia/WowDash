using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using System.Net.Http;
using System.Threading.Tasks;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;

namespace WowDash.IntegrationTests
{
    public class IntegrationTestBase
    {
        private CustomWebApplicationFactory _factory;
        private Checkpoint _checkpoint;

        public HttpClient Client { get; set; }

        public System.Guid TestUserGuid { get; set; }

        [OneTimeSetUp]
        public void RunBeforeTheseTests()
        {
            _factory = new CustomWebApplicationFactory();
            Client = _factory.CreateClient();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };
        }

        [SetUp]
        public async System.Threading.Tasks.Task RunBeforeEachTest()
        {
            // Haven't figured out how to get connection string from factory
            var connectionString = "Server=(LocalDB)\\MSSQLLocalDB;Database=WowDash-Testing;Trusted_Connection=True;MultipleActiveResultSets=true";
            await _checkpoint.Reset(connectionString);
        }

        [OneTimeTearDown]
        public void RunAfterTheseTests()
        {
            Client.Dispose();
            _factory.Dispose();
        }

        public async System.Threading.Tasks.Task AddAThingAsync()
        {
            var player = new Player();
            using var scope = _factory.Server.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var user = context.Add(player);

            TestUserGuid = user.Entity.Id;  // Just trying to get it to save something

            await context.SaveChangesAsync();

            // Leaves leftover player at the end of testing stray in DB
            // When I try to get one, it's Guid.Empty
        }
    }
}
