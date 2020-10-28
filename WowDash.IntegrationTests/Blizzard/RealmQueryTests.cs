using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WowDash.ApplicationCore.Models;

namespace WowDash.IntegrationTests.Blizzard
{
    [TestFixture]
    public class RealmQueryTests : IntegrationTestBase
    {
        [Test]
        public async Task GetRealmIndex_ReturnsRealms() { 

            // Act
            var httpResponse = await Client.GetAsync($"/api/realms");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<IEnumerable<Realm>>(response, options);

            // Assert
            result.Should().NotBeEmpty();
            result.FirstOrDefault().Name.Should().NotBeNullOrEmpty();
        }
    }
}
