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
        public async System.Threading.Tasks.Task InitializeTaskEndpoint_AddsTaskToDatabaseAsync()
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

            var foundTask = FindAsync<Task>(result).Result;

            // Assert
            foundTask.Should().NotBeNull();
            foundTask.PlayerId.Should().Be(defaultPlayerId);
            foundTask.TaskType.Should().Be(TaskType.Achievement);
        }
    }
}
