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
        // Haven't figured out how to get connection string from factory
        private string _connectionString = "Server=(LocalDB)\\MSSQLLocalDB;Database=WowDash-Testing;Trusted_Connection=True;MultipleActiveResultSets=true";

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
            await _checkpoint.Reset(_connectionString);
        }

        [OneTimeTearDown]
        public async System.Threading.Tasks.Task RunAfterTheseTests()
        {
            await _checkpoint.Reset(_connectionString); // Removes stray entities leftover from the last test

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

            // When I try to get one, it's Guid.Empty
        }
    }
}
