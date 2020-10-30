using FluentAssertions;
using NUnit.Framework;
using System.Text.Json;
using WowDash.ApplicationCore.DTO.Responses;
using WowDash.ApplicationCore.Entities;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.IntegrationTests.Players
{
    public class PlayerQueryTests : IntegrationTestBase
    {
        [Test]
        public async System.Threading.Tasks.Task GetPlayerProfile_ReturnsPlayerProfile()
        {
            // Arrange
            var expectedDefaultTaskType = TaskType.Collectible;
            var expectedDisplayName = "Thrall";
            var expectedDefaultRealm = "thrall";

            var player = new Player()
            {
                DisplayName = expectedDisplayName,
                DefaultRealm = expectedDefaultRealm,
                DefaultTaskType = expectedDefaultTaskType
            };

            var addedPlayer = await AddAsync(player);

            // Act
            var httpResponse = await Client.GetAsync($"/api/players/{addedPlayer.Id}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<GetPlayerProfileResponse>(response, options);

            // Assert
            result.PlayerId.Should().Be(player.Id);
            result.DisplayName.Should().Be(expectedDisplayName);
            result.DefaultTaskType.Should().Be(expectedDefaultTaskType);
            result.DefaultRealm.Should().Be(expectedDefaultRealm);
        }
    }
}
