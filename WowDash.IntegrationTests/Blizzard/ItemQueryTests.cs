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
    public class ItemQueryTests : IntegrationTestBase
    {
        [Test]
        public async Task GetItem_ReturnsItem()
        {
            var expectedGameId = 49319;
            var expectedName = "Dragonstalker's Helmet";
            var expectedSubclass = "Mail";

            // Act
            var httpResponse = await Client.GetAsync($"/api/items/{expectedGameId}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<Item>(response, options);

            // Assert
            result.Id.Should().Be(expectedGameId);
            result.Name.Should().Be(expectedName);
            result.Subclass.Name.Should().Be(expectedSubclass);
        }

        [Test]
        public async Task SearchItemsByName_ReturnsItemList()
        {
            var searchTerm = "thunder";

            // Act
            var httpResponse = await Client.GetAsync($"/api/items/search/{searchTerm}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<IEnumerable<Item>>(response, options);

            // Assert
            result.Should().NotBeEmpty();
            result.All(a => a.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).Should().BeTrue();
        }
    }
}
