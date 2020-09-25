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
    public class ItemSetQueryTests : IntegrationTestBase
    {
        [Test]
        public async Task GetItemSet_ReturnsItemSet()
        {
            var expectedGameId = 857;
            var expectedName = "VanCleef's Battlegear";

            // Act
            var httpResponse = await Client.GetAsync($"/api/item-sets/{expectedGameId}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<ItemSet>(response, options);

            // Assert
            result.Id.Should().Be(expectedGameId);
            result.Name.Should().Be(expectedName);
        }

        [Test]
        public async Task SearchItemSetsByName_ReturnsList()
        {
            var searchTerm = "thunder";

            // Act
            var httpResponse = await Client.GetAsync($"/api/item-sets/search/{searchTerm}");

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
    }
}
