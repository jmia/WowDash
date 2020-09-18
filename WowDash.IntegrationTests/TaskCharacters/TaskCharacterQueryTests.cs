using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using WowDash.ApplicationCore.DTO.Responses;
using WowDash.ApplicationCore.Entities;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.IntegrationTests.TaskCharacters
{
    [TestFixture]
    public class TaskCharacterQueryTests : IntegrationTestBase
    {
        internal Guid defaultTaskId;
        internal Guid defaultCharacterId;
        internal Guid defaultPlayerId;

        [SetUp]
        public async System.Threading.Tasks.Task Setup()
        {
            var defaultPlayer = await AddAsync(new Player() { DisplayName = "Jen" });
            defaultPlayerId = defaultPlayer.Id;
            var defaultTask = await AddAsync(new ApplicationCore.Entities.Task(defaultPlayer.Id, TaskType.General));
            defaultTaskId = defaultTask.Id;
            var defaultCharacter = await AddAsync(new Character() { PlayerId = defaultPlayer.Id });
            defaultCharacterId = defaultCharacter.Id;
        }

        [Test]
        public async System.Threading.Tasks.Task GetTaskCharacterById_ReturnsTaskCharacter()
        {
            // Arrange
            var taskCharacter = await AddAsync(new TaskCharacter(defaultCharacterId, defaultTaskId));

            // Act
            var httpResponse = await Client.GetAsync($"/api/task-characters/{taskCharacter.CharacterId}:{taskCharacter.TaskId}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<GetTaskCharacterByIdResponse>(response, options);

            var foundTaskCharacter = await FindAsync<TaskCharacter>(result.CharacterId, result.TaskId);

            // Assert
            foundTaskCharacter.Should().NotBeNull();
            foundTaskCharacter.TaskId.Should().Be(defaultTaskId);
            foundTaskCharacter.CharacterId.Should().Be(defaultCharacterId);
            foundTaskCharacter.IsActive.Should().BeTrue();
        }

        [Test]
        public void GetTaskCharactersByTaskId_ReturnsTaskCharacters()
        {

        }

        [Test]
        public void GetTaskCharactersByCharacterId_ReturnsTaskCharacters()
        {

        }
    }
}
