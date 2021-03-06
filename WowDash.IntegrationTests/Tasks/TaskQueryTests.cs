﻿using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WowDash.ApplicationCore.DTO.Common;
using WowDash.ApplicationCore.DTO.Responses;
using WowDash.ApplicationCore.Entities;
using static WowDash.ApplicationCore.Common.Enums;
using Task = WowDash.ApplicationCore.Entities.Task;

namespace WowDash.IntegrationTests.Tasks
{
    [TestFixture]
    public class TaskQueryTests : IntegrationTestBase
    {
        internal Guid defaultPlayerId;

        [SetUp]
        public async System.Threading.Tasks.Task Setup()
        {
            var defaultPlayer = await AddAsync(new Player() { DisplayName = "Jen" });
            defaultPlayerId = defaultPlayer.Id;
        }

        [Test]
        public async System.Threading.Tasks.Task GetTaskById_ReturnsTask()
        {
            // Arrange
            var expectedTaskType = TaskType.Collectible;
            var expectedDescription = "Get Midnight's Eternal Reins from Karazhan";
            var expectedGdrDescription = "Midnight's Eternal Reins";
            var expectedGameDataReferences = new List<GameDataReference>()
            {
                new GameDataReference(142236, GameDataReference.GameDataType.Item, "Mount", expectedGdrDescription)
            };
            var expectedCollectibleType = CollectibleType.Mount;
            var expectedSource = Source.Dungeon;
            var expectedPriority = Priority.Medium;
            var expectedRefreshFrequency = RefreshFrequency.Weekly;

            var task = new Task(defaultPlayerId, expectedTaskType)
            {
                Description = expectedDescription,
                GameDataReferences = expectedGameDataReferences,
                CollectibleType = expectedCollectibleType,
                Source = expectedSource,
                Priority = expectedPriority,
                RefreshFrequency = expectedRefreshFrequency
            };

            await AddAsync(task);

            // Act
            var httpResponse = await Client.GetAsync($"/api/tasks/{task.Id}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<TaskResponse>(response, options);

            // Assert
            result.TaskId.Should().Be(task.Id);
            result.TaskType.Should().Be(expectedTaskType);
            result.Description.Should().Be(expectedDescription);
            result.GameDataReferences.Should().NotBeEmpty().And.HaveCount(1);
            result.GameDataReferences.Any(gdr => gdr.Description.Equals(expectedGdrDescription));
            result.CollectibleType.Should().Be(expectedCollectibleType);
            result.Source.Should().Be(expectedSource);
            result.Priority.Should().Be(expectedPriority);
            result.RefreshFrequency.Should().Be(expectedRefreshFrequency);
        }

        [Test]
        public async System.Threading.Tasks.Task GetTasksWithPlayerId_ReturnsTask()
        {
            // Arrange
            var expectedTaskType = TaskType.Collectible;
            var expectedDescription = "Get Midnight's Eternal Reins from Karazhan";
            var expectedGdrDescription = "Midnight's Eternal Reins";
            var expectedGameDataReferences = new List<GameDataReference>()
            {
                new GameDataReference(142236, GameDataReference.GameDataType.Item, "Mount", expectedGdrDescription)
            };
            var expectedCollectibleType = CollectibleType.Mount;
            var expectedSource = Source.Dungeon;
            var expectedPriority = Priority.Medium;
            var expectedRefreshFrequency = RefreshFrequency.Weekly;

            var task = new Task(defaultPlayerId, expectedTaskType)
            {
                Description = expectedDescription,
                GameDataReferences = expectedGameDataReferences,
                CollectibleType = expectedCollectibleType,
                Source = expectedSource,
                Priority = expectedPriority,
                RefreshFrequency = expectedRefreshFrequency
            };

            await AddAsync(task);

            var queryString = $"playerId={defaultPlayerId}";

            // Act
            var httpResponse = await Client.GetAsync($"/api/tasks?{queryString}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<GetTasksResponse>(response, options);

            // Assert
            result.PlayerId.Should().Be(defaultPlayerId);
            result.Tasks.Should().NotBeEmpty().And.HaveCount(1);

            var foundTask = result.Tasks.FirstOrDefault();

            foundTask.Description.Should().Be(expectedDescription);
            foundTask.GameDataReferences.Should().NotBeEmpty().And.HaveCount(1);
        }

        [Test]
        public async System.Threading.Tasks.Task GetPlayerDungeonsList_ReturnsList()
        {
            // Arrange
            var expectedTaskType = TaskType.Collectible;
            var expectedGdrDescription = "Karazhan";
            var expectedGameDataReferences = new List<GameDataReference>()
            {
                new GameDataReference(103, GameDataReference.GameDataType.Dungeon, null, expectedGdrDescription)
            };

            var task = new Task(defaultPlayerId, expectedTaskType)
            {
                GameDataReferences = expectedGameDataReferences
            };

            await AddAsync(task);

            // Act
            var httpResponse = await Client.GetAsync($"/api/tasks/dungeon-index/{defaultPlayerId}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<ICollection<FilterListSourceResponse>>(response, options);

            // Assert
            Assert.IsInstanceOf<ICollection<FilterListSourceResponse>>(result);
            result.Count.Should().Be(1);
            result.Any(d => d.Name.Equals(expectedGdrDescription)).Should().BeTrue();
        }

        [Test]
        public async System.Threading.Tasks.Task GetPlayerZonesList_ReturnsList()
        {
            // Arrange
            var expectedTaskType = TaskType.Collectible;
            var expectedGdrDescription = "Deadwind Pass";
            var expectedGameDataReferences = new List<GameDataReference>()
            {
                new GameDataReference(103, GameDataReference.GameDataType.Zone, null, expectedGdrDescription)
            };

            var task = new Task(defaultPlayerId, expectedTaskType)
            {
                GameDataReferences = expectedGameDataReferences
            };

            await AddAsync(task);

            // Act
            var httpResponse = await Client.GetAsync($"/api/tasks/zone-index/{defaultPlayerId}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<ICollection<FilterListSourceResponse>>(response, options);

            // Assert
            Assert.IsInstanceOf<ICollection<FilterListSourceResponse>>(result);
            result.Count.Should().Be(1);
            result.Any(d => d.Name.Equals(expectedGdrDescription)).Should().BeTrue();
        }
    }
}
