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
    public class AchievementQueryTests : IntegrationTestBase
    {
        // TODO: These suites don't need access to the DbContext, but
        // they do need access to setup for configuration properties
        // how to split this up?

        [Test]
        public async Task GetAchievement_ReturnsAchievement()
        {
            var expectedGameId = 9058;
            var expectedName = "Leeeeeeeeeeeeeroy...?";
            var expectedDescription = "Assist Leeroy Jenkins in recovering his Devout shoulders in Upper Blackrock Spire on Heroic difficulty.";

            // Act
            var httpResponse = await Client.GetAsync($"/api/achievements/{expectedGameId}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<Achievement>(response, options);

            // Assert
            result.Id.Should().Be(expectedGameId);
            result.Name.Should().Be(expectedName);
            result.Description.Should().Be(expectedDescription);
        }

        [Test]
        public async Task SearchAchievementsByName_ReturnsAchievementList()
        {
            var searchTerm = "angler";

            // Act
            var httpResponse = await Client.GetAsync($"/api/achievements/search/{searchTerm}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<IEnumerable<Achievement>>(response, options);

            // Assert
            result.Should().NotBeEmpty();
            result.All(a => a.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).Should().BeTrue();
        }
    }
}
