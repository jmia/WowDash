using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using WowDash.ApplicationCore.Entities;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.IntegrationTests.TaskCharacters
{
    [TestFixture]
    public class TaskCharacterCommandTests : IntegrationTestBase
    {
        internal Guid defaultTaskId;
        internal Guid defaultCharacterId;

        [SetUp]
        public async System.Threading.Tasks.Task Setup()
        {
            var defaultPlayer = await AddAsync(new Player() { DisplayName = "Jen" });
            var defaultTask = await AddAsync(new Task(defaultPlayer.Id, TaskType.General));
            defaultTaskId = defaultTask.Id;
            var defaultCharacter = await AddAsync(new Character() { PlayerId = defaultPlayer.Id });
            defaultCharacterId = defaultCharacter.Id;
        }

        [Test]
        public async System.Threading.Tasks.Task AddCharacterToTask_CreatesNewTaskCharacter()
        {
            // {
            //    "characterId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            // };

            // Arrange
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
                    writer.WriteString("characterId", defaultCharacterId);
                    writer.WriteString("taskId", defaultTaskId);
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PutAsync($"/api/task-characters/", content);

            httpResponse.EnsureSuccessStatusCode();

            var foundTaskCharacter = await FindAsync<TaskCharacter>(defaultCharacterId, defaultTaskId);

            // Assert
            foundTaskCharacter.Should().NotBeNull();
            foundTaskCharacter.TaskId.Should().Be(defaultTaskId);
            foundTaskCharacter.CharacterId.Should().Be(defaultCharacterId);
        }

        [Test]
        public async System.Threading.Tasks.Task RemoveCharacterFromTask_DeletesTaskCharacter()
        {
            // Arrange
            await AddAsync(new TaskCharacter(defaultCharacterId, defaultTaskId));

            // Act
            var httpResponse = await Client.DeleteAsync($"/api/task-characters/{defaultCharacterId}:{defaultTaskId}");

            httpResponse.EnsureSuccessStatusCode();

            var foundTaskCharacter = await FindAsync<TaskCharacter>(defaultCharacterId, defaultTaskId);

            // Assert
            foundTaskCharacter.Should().BeNull();
        }
    }
}
