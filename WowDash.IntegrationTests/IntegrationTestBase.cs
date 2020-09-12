using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WowDash.Infrastructure;

namespace WowDash.IntegrationTests
{
    /// <summary>
    /// Cobbled together over many bleary hours
    /// using actual human blood and sweat and some snippets from
    /// https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1#customize-webapplicationfactory
    /// and https://medium.com/@daniel.edwards_82928/using-webapplicationfactory-with-nunit-817a616e26f9
    /// and https://github.com/jasontaylordev/CleanArchitecture
    /// </summary>
    public class IntegrationTestBase
    {
        private static CustomWebApplicationFactory _factory;
        private Checkpoint _checkpoint;

        // Haven't figured out how to get connection string from factory
        private string _connectionString = 
            "Server=(LocalDB)\\MSSQLLocalDB;Database=WowDash-Testing;Trusted_Connection=True;MultipleActiveResultSets=true";

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
        public async Task RunBeforeEachTest()
        {
            await _checkpoint.Reset(_connectionString);
        }

        [OneTimeTearDown]
        public async Task RunAfterTheseTests()
        {
            // Removes stray entities leftover from the last test
            await _checkpoint.Reset(_connectionString);

            Client.Dispose();
            _factory.Dispose();
        }

        public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = _factory.Server.Services.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        public static async Task<TEntity> AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _factory.Server.Services.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();

            return entity;
        }
    }
}
