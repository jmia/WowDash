using NUnit.Framework;
using Respawn;
using System.Net.Http;
using System.Threading.Tasks;

namespace WowDash.IntegrationTests
{
    public class IntegrationTestBase
    {
        private CustomWebApplicationFactory _factory;
        private Checkpoint _checkpoint;

        public HttpClient Client { get; set; }

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
        public async Task RunBeforeEachTestAsync()
        {
            var connectionString = "Server=(LocalDB)\\MSSQLLocalDB;Database=WowDash-Testing;Trusted_Connection=True;MultipleActiveResultSets=true";
            await _checkpoint.Reset(connectionString);
        }

        [OneTimeTearDown]
        public void RunAfterTheseTests()
        {
            Client.Dispose();
            _factory.Dispose();
        }
    }
}
