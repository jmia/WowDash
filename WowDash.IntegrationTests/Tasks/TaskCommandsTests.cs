using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
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
            var expectedTaskType = (int)TaskType.Achievement;
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
                    writer.WriteNumber("taskType", expectedTaskType);
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
            foundTask.TaskType.Should().Be(TaskType.Achievement);
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
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //      "description": "string",
            //      "refreshFrequency": 0,
            //      "priority": 0
            //}
        }

        [Test]
        public async System.Threading.Tasks.Task SetAchievementTaskDetails_UpdatesTask()
        {
            //{
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //    "description": "string",
            //    "priority": 0
            //}
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
        }

        [Test]
        public async System.Threading.Tasks.Task SetTaskCollectibleTypeAndSource_UpdatesTask()
        {
            //{
            //  "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //  "collectibleType": 0,
            //  "source": 0
            //}
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
        }

        [Test]
        public async System.Threading.Tasks.Task SetTaskNotes_UpdatesTask()
        {
            //{
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //    "notes": "string"
            //}
        }

        [Test]
        public async System.Threading.Tasks.Task AddTaskToFavourites_UpdatesTask()
        {
            //{
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            //}
        }

        [Test]
        public async System.Threading.Tasks.Task RemoveTaskFromFavourites_UpdatesTask()
        {
            //{
            //    "taskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            //}
        }
    }
}
