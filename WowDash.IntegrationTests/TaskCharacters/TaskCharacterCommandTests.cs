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
        internal Guid defaultPlayerId;

        [SetUp]
        public async System.Threading.Tasks.Task Setup()
        {
            var defaultPlayer = await AddAsync(new Player() { DisplayName = "Jen" });
            defaultPlayerId = defaultPlayer.Id;
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
        }

        [Test]
        public async System.Threading.Tasks.Task RemoveCharacterFromTask_DeletesTaskCharacter()
        {
            // DELETE /api/task-characters/{characterId}:{taskId}

            // Arrange
            await AddAsync(new TaskCharacter(defaultCharacterId, defaultTaskId));

            // Act
            var httpResponse = await Client.DeleteAsync($"/api/task-characters/{defaultCharacterId}:{defaultTaskId}");

            httpResponse.EnsureSuccessStatusCode();

            var foundTaskCharacter = await FindAsync<TaskCharacter>(defaultCharacterId, defaultTaskId);

            // Assert
            foundTaskCharacter.Should().BeNull();
        }

        [Test]
        public async System.Threading.Tasks.Task SetAttemptComplete_UpdatesTaskCharacter()
        {
            // {
            //    "characterId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            // };

            // Arrange
            await AddAsync(new TaskCharacter(defaultCharacterId, defaultTaskId));

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
            var httpResponse = await Client.PatchAsync($"/api/task-characters/complete", content);

            httpResponse.EnsureSuccessStatusCode();

            var foundTaskCharacter = await FindAsync<TaskCharacter>(defaultCharacterId, defaultTaskId);

            // Assert
            foundTaskCharacter.Should().NotBeNull();
            foundTaskCharacter.IsActive.Should().BeFalse();
        }

        [Test]
        public async System.Threading.Tasks.Task SetAttemptIncomplete_UpdatesTaskCharacter()
        {
            // {
            //    "characterId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            // };

            // Arrange
            await AddAsync(new TaskCharacter(defaultCharacterId, defaultTaskId) { IsActive = false });

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
            var httpResponse = await Client.PatchAsync($"/api/task-characters/revert", content);

            httpResponse.EnsureSuccessStatusCode();

            var foundTaskCharacter = await FindAsync<TaskCharacter>(defaultCharacterId, defaultTaskId);

            // Assert
            foundTaskCharacter.Should().NotBeNull();
            foundTaskCharacter.IsActive.Should().BeTrue();
        }

        [Test]
        public async System.Threading.Tasks.Task RefreshDailyTasks_UpdatesTaskCharacter()
        {
            // {
            //    "characterId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            // };

            // Arrange
            var dailyTask = await AddAsync(new Task(defaultPlayerId, TaskType.Collectible) { RefreshFrequency = RefreshFrequency.Daily });
            await AddAsync(new TaskCharacter(defaultCharacterId, dailyTask.Id) { IsActive = false });

            string json;

            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using (var stream = new MemoryStream())
            {
                // Empty object
                using (var writer = new Utf8JsonWriter(stream, options))
                {
                    writer.WriteStartObject();
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PostAsync($"/api/task-characters/refresh/daily", content);

            httpResponse.EnsureSuccessStatusCode();

            var foundTaskCharacter = await FindAsync<TaskCharacter>(defaultCharacterId, dailyTask.Id);

            // Assert
            foundTaskCharacter.Should().NotBeNull();
            foundTaskCharacter.IsActive.Should().BeTrue();
        }

        [Test]
        public async System.Threading.Tasks.Task RefreshWeeklyTasks_UpdatesTaskCharacter()
        {
            // {
            //    "characterId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            // };

            // Arrange
            var dailyTask = await AddAsync(new Task(defaultPlayerId, TaskType.Collectible) { RefreshFrequency = RefreshFrequency.Weekly });
            await AddAsync(new TaskCharacter(defaultCharacterId, dailyTask.Id) { IsActive = false });

            string json;

            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using (var stream = new MemoryStream())
            {
                // Empty object
                using (var writer = new Utf8JsonWriter(stream, options))
                {
                    writer.WriteStartObject();
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PostAsync($"/api/task-characters/refresh/weekly", content);

            httpResponse.EnsureSuccessStatusCode();

            var foundTaskCharacter = await FindAsync<TaskCharacter>(defaultCharacterId, dailyTask.Id);

            // Assert
            foundTaskCharacter.Should().NotBeNull();
            foundTaskCharacter.IsActive.Should().BeTrue();
        }
    }
}
