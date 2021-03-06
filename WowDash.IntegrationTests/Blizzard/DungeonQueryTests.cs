﻿using FluentAssertions;
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
    public class DungeonQueryTests : IntegrationTestBase
    {
        [Test]
        public async Task GetDungeon_ReturnsDungeon()
        {
            var expectedGameId = 751;
            var expectedName = "Black Temple";

            // Act
            var httpResponse = await Client.GetAsync($"/api/dungeons/{expectedGameId}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<Dungeon>(response, options);

            // Assert
            result.Id.Should().Be(expectedGameId);
            result.Name.Should().Be(expectedName);
        }

        [Test]
        public async Task SearchDungeonsByName_ReturnsList()
        {
            var searchTerm = "karazhan";

            // Act
            var httpResponse = await Client.GetAsync($"/api/dungeons/search/{searchTerm}");

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
