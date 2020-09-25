using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WowDash.ApplicationCore.Models;

namespace WowDash.IntegrationTests.Blizzard
{
    [TestFixture]
    public class BossQueryTests : IntegrationTestBase
    {
        [Test]
        public async Task GetBoss_ReturnsBoss()
        {
            var expectedGameId = 1590;
            var expectedName = "Illidan Stormrage";

            // Act
            var httpResponse = await Client.GetAsync($"/api/bosses/{expectedGameId}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<Boss>(response, options);

            // Assert
            result.Id.Should().Be(expectedGameId);
            result.Name.Should().Be(expectedName);
        }

        [Test]
        public async Task SearchBossesByBossName_ReturnsList()
        {
            var searchTerm = "prince";

            // Act
            var httpResponse = await Client.GetAsync($"/api/bosses/search/{searchTerm}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<IEnumerable<SearchResult>>(response, options);

            // Assert
            result.Should().NotBeEmpty();
            result.All(a => a.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).Should().BeTrue();
        }

        [Test]
        public async Task SearchBossesByDungeonName_ReturnsList()
        {
            var searchTerm = "karazhan";

            // Act
            var httpResponse = await Client.GetAsync($"/api/bosses/search/dungeon/{searchTerm}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<IEnumerable<SearchResult>>(response, options);

            // Assert
            result.Should().NotBeEmpty();
            result.Any(a => a.Name.Contains("Moroes")).Should().BeTrue();
            result.Any(a => a.Name.Contains("Attumen the Huntsman")).Should().BeTrue();
            result.Any(a => a.Name.Contains("Opera Hall")).Should().BeTrue();
        }
    }
}
