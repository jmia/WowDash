using FluentAssertions;
using NUnit.Framework;
using System.Text.Json;
using WowDash.ApplicationCore.Entities;
using WowDash.IntegrationTests.Responses;

namespace WowDash.IntegrationTests.Players
{
    [TestFixture]
    public class GetPlayersTests : IntegrationTestBase
    {
        [Test]
        public async System.Threading.Tasks.Task GetPlayers_ShouldFindSomething()
        {
            // Arrange
            var player = await AddAsync(new Player() { DisplayName = "TestPlayer" });
            var expectedId = player.Id;

            // Act
            var httpResponse = await Client.GetAsync("/api/Players/" + expectedId); // TODO: Will need to change capitalization later

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<GetPlayerResponse>(response);

            // Assert
            result.Id.Should().Be(expectedId);
        }
    }
}
