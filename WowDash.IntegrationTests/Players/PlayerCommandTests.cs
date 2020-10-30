using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using WowDash.ApplicationCore.Entities;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.IntegrationTests.Players
{
    public class PlayerCommandTests : IntegrationTestBase
    {
        [Test]
        public async System.Threading.Tasks.Task SetPlayerProfileEndpoint_UpdatesPlayer()
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

            await AddAsync(player);

            string json;

            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream, options))
                {
                    writer.WriteStartObject();
                    writer.WriteString("playerId", player.Id);
                    writer.WriteString("displayName", expectedDisplayName);
                    writer.WriteNumber("defaultTaskType", (int)expectedDefaultTaskType);
                    writer.WriteString("defaultRealm", expectedDefaultRealm);
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PutAsync("/api/players", content);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundPlayer = await FindAsync<Player>(result);

            // Assert
            foundPlayer.Id.Should().Be(player.Id);
            foundPlayer.DisplayName.Should().Be(expectedDisplayName);
            foundPlayer.DefaultTaskType.Should().Be(expectedDefaultTaskType);
            foundPlayer.DefaultRealm.Should().Be(expectedDefaultRealm);
        }
    }
}
