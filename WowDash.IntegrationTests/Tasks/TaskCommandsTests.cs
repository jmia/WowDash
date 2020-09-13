using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using WowDash.ApplicationCore.Entities;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.IntegrationTests.Tasks
{
    [TestFixture]
    public class TaskCommandsTests : IntegrationTestBase
    {
        internal Guid defaultPlayerId;

        [SetUp]
        public async System.Threading.Tasks.Task Setup()
        {
            var defaultPlayer = await AddAsync(new Player() { DisplayName = "Jen" });
            defaultPlayerId = defaultPlayer.Id;
        }

        [Test]
        public async System.Threading.Tasks.Task InitializeTaskEndpoint_AddsTask()
        {
            // {
            //    "playerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //    "taskType": 0
            // };

            // Arrange
            var expectedTaskType = TaskType.Achievement;
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
                    writer.WriteString("playerId", defaultPlayerId);
                    writer.WriteNumber("taskType", (int)expectedTaskType);
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PostAsync("/api/tasks", content);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundTask = await FindAsync<Task>(result);

            // Assert
            foundTask.Should().NotBeNull();
            foundTask.PlayerId.Should().Be(defaultPlayerId);
            foundTask.TaskType.Should().Be(expectedTaskType);
        }

        [Test]
        public async System.Threading.Tasks.Task DeleteTask_DeletesTask()
        {
            //{
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            //}

            // Arrange
            var task = await AddAsync(new Task(defaultPlayerId, TaskType.General));

            // Act
            var httpResponse = await Client.DeleteAsync($"/api/tasks/{task.Id}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundTask = FindAsync<Task>(result).Result;

            // Assert
            foundTask.Should().BeNull();
        }

        [Test]
        public async System.Threading.Tasks.Task SetGeneralTaskDetails_UpdatesTask()
        {
            //{
            //      "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //      "description": "string",
            //      "refreshFrequency": 0,
            //      "priority": 0
            //}

            // Arrange
            var task = await AddAsync(new Task(defaultPlayerId, TaskType.General));

            var expectedDescription = "Complete PVP Weekly Quest";
            var expectedRefreshFrequency = RefreshFrequency.Weekly;
            var expectedPriority = Priority.Highest;
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
                    writer.WriteString("taskId", task.Id);
                    writer.WriteString("description", expectedDescription);
                    writer.WriteNumber("refreshFrequency", (int)expectedRefreshFrequency);
                    writer.WriteNumber("priority", (int)expectedPriority);
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PatchAsync("/api/tasks/general/details", content);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundTask = await FindAsync<Task>(result);

            // Assert
            foundTask.Should().NotBeNull();
            foundTask.PlayerId.Should().Be(defaultPlayerId);
            foundTask.Description.Should().Be(expectedDescription);
            foundTask.RefreshFrequency.Should().Be(expectedRefreshFrequency);
            foundTask.Priority.Should().Be(expectedPriority);
        }

        [Test]
        public async System.Threading.Tasks.Task SetAchievementTaskDetails_UpdatesTask()
        {
            //{
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //    "description": "string",
            //    "priority": 0
            //}

            // Arrange
            var task = await AddAsync(new Task(defaultPlayerId, TaskType.General));

            var expectedDescription = "Reach Level 100";
            var expectedPriority = Priority.Low;
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
                    writer.WriteString("taskId", task.Id);
                    writer.WriteString("description", expectedDescription);
                    writer.WriteNumber("priority", (int)expectedPriority);
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PatchAsync("/api/tasks/achievement/details", content);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundTask = await FindAsync<Task>(result);

            // Assert
            foundTask.Should().NotBeNull();
            foundTask.PlayerId.Should().Be(defaultPlayerId);
            foundTask.Description.Should().Be(expectedDescription);
            foundTask.RefreshFrequency.Should().Be(RefreshFrequency.Never);
            foundTask.Priority.Should().Be(expectedPriority);
        }

        [Test]
        public async System.Threading.Tasks.Task SetCollectibleTaskDetails_UpdatesTask()
        {

            //{
            //  "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //  "description": "string",
            //  "refreshFrequency": 0,
            //  "priority": 0
            //}

            // Arrange
            var task = await AddAsync(new Task(defaultPlayerId, TaskType.General));

            var expectedDescription = "Run Tempest Keep for Ashes of Al'ar";
            var expectedRefreshFrequency = RefreshFrequency.Weekly;
            var expectedPriority = Priority.High;
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
                    writer.WriteString("taskId", task.Id);
                    writer.WriteString("description", expectedDescription);
                    writer.WriteNumber("refreshFrequency", (int)expectedRefreshFrequency);
                    writer.WriteNumber("priority", (int)expectedPriority);
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PatchAsync("/api/tasks/collectible/details", content);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundTask = await FindAsync<Task>(result);

            // Assert
            foundTask.Should().NotBeNull();
            foundTask.PlayerId.Should().Be(defaultPlayerId);
            foundTask.Description.Should().Be(expectedDescription);
            foundTask.RefreshFrequency.Should().Be(expectedRefreshFrequency);
            foundTask.Priority.Should().Be(expectedPriority);
        }

        [Test]
        public async System.Threading.Tasks.Task SetTaskCollectibleTypeAndSource_UpdatesTask()
        {
            //{
            //  "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //  "collectibleType": 0,
            //  "source": 0
            //}

            // Arrange
            var task = await AddAsync(new Task(defaultPlayerId, TaskType.General));

            var expectedCollectibleType = CollectibleType.Mount;
            var expectedSource = Source.Dungeon;
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
                    writer.WriteString("taskId", task.Id);
                    writer.WriteNumber("collectibleType", (int)expectedCollectibleType);
                    writer.WriteNumber("source", (int)expectedSource);
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PatchAsync("/api/tasks/collectible/type-source", content);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundTask = await FindAsync<Task>(result);

            // Assert
            foundTask.Should().NotBeNull();
            foundTask.PlayerId.Should().Be(defaultPlayerId);
            foundTask.CollectibleType.Should().Be(expectedCollectibleType);
            foundTask.Source.Should().Be(expectedSource);
        }

        [Test]
        public async System.Threading.Tasks.Task SetGameDataReferences_UpdatesTask()
        {
            //{
            //  "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //  "gameDataReferenceItems": [
            //    {
            //      "id": 0,
            //      "gameId": 0,
            //      "type": 0,
            //      "subclass": "string",
            //      "description": "string"
            //    }
            //  ]
            //}

            // Arrange
            var task = await AddAsync(new Task(defaultPlayerId, TaskType.Collectible));

            var expectedType = GameDataReference.GameDataType.Item;
            var expectedDescription = "Piccolo of the Flaming Fire";
            var expectedSubclass = "toy";
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
                    writer.WriteString("taskId", task.Id);
                    writer.WriteStartArray("gameDataReferenceItems");
                        writer.WriteStartObject();
                            writer.WriteNumber("id", 0);
                            writer.WriteNumber("gameId", 13379);
                            writer.WriteNumber("type", (int)expectedType);
                            writer.WriteString("subclass", expectedSubclass);
                            writer.WriteString("description", expectedDescription);
                        writer.WriteEndObject();
                    writer.WriteEndArray();
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PutAsync("/api/tasks/references", content);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundTask = await FindAsync<Task>(result);

            // Assert
            foundTask.Should().NotBeNull();
            foundTask.GameDataReferences.Count.Should().Be(1);
            foundTask.GameDataReferences.Any(gdr => gdr.Id != 0).Should().BeTrue();
            foundTask.GameDataReferences.FirstOrDefault().Description.Should().Be(expectedDescription);
            foundTask.GameDataReferences.FirstOrDefault().Subclass.Should().Be(expectedSubclass);
            foundTask.GameDataReferences.FirstOrDefault().Type.Should().Be(expectedType);
            foundTask.PlayerId.Should().Be(defaultPlayerId);
        }

        [Test]
        public async System.Threading.Tasks.Task SetTaskNotes_UpdatesTask()
        {
            //{
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //    "notes": "string"
            //}

            // Arrange
            var task = await AddAsync(new Task(defaultPlayerId, TaskType.General));

            var expectedNotes = "Ash said she would do this one with you.";
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
                    writer.WriteString("taskId", task.Id);
                    writer.WriteString("notes", expectedNotes);
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PatchAsync("/api/tasks/notes", content);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundTask = await FindAsync<Task>(result);

            // Assert
            foundTask.Should().NotBeNull();
            foundTask.PlayerId.Should().Be(defaultPlayerId);
            foundTask.Notes.Should().Be(expectedNotes);
        }

        [Test]
        public async System.Threading.Tasks.Task AddTaskToFavourites_UpdatesTask()
        {
            //{
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            //}

            // Arrange
            var task = await AddAsync(new Task(defaultPlayerId, TaskType.General));

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
                    writer.WriteString("taskId", task.Id);
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PatchAsync("/api/tasks/favourites/add", content);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundTask = await FindAsync<Task>(result);

            // Assert
            foundTask.Should().NotBeNull();
            foundTask.PlayerId.Should().Be(defaultPlayerId);
            foundTask.IsFavourite.Should().BeTrue();
        }

        [Test]
        public async System.Threading.Tasks.Task RemoveTaskFromFavourites_UpdatesTask()
        {
            //{
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            //}

            // Arrange
            var task = await AddAsync(new Task(defaultPlayerId, TaskType.General) { IsFavourite = true });

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
                    writer.WriteString("taskId", task.Id);
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PatchAsync("/api/tasks/favourites/remove", content);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundTask = await FindAsync<Task>(result);

            // Assert
            foundTask.Should().NotBeNull();
            foundTask.PlayerId.Should().Be(defaultPlayerId);
            foundTask.IsFavourite.Should().BeFalse();
        }
    }
}
