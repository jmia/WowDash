using FluentAssertions;
using NUnit.Framework;
using System;
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
            // Arrange, Act
            var httpResponse = await Client.PutAsync($"/api/task-characters/add/task/{defaultTaskId}/character/{defaultCharacterId}", null);

            httpResponse.EnsureSuccessStatusCode();

            var foundTaskCharacter = await FindAsync<TaskCharacter>(defaultCharacterId, defaultTaskId);

            // Assert
            foundTaskCharacter.Should().NotBeNull();
            foundTaskCharacter.TaskId.Should().Be(defaultTaskId);
            foundTaskCharacter.CharacterId.Should().Be(defaultCharacterId);
        }

        [Test]
        public async System.Threading.Tasks.Task AddCharacterToTask_AlternateRoute_CreatesNewTaskCharacter()
        {
            // Arrange, Act
            var httpResponse = await Client.PutAsync($"/api/task-characters/add/character/{defaultCharacterId}/task/{defaultTaskId}", null);

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
            // Arrange, Act
            var httpResponse = await Client.PutAsync($"/api/task-characters/add/character/{defaultCharacterId}/task/{defaultTaskId}", null);

            httpResponse.EnsureSuccessStatusCode();

            var foundTaskCharacter = await FindAsync<TaskCharacter>(defaultCharacterId, defaultTaskId);

            // Assert
            foundTaskCharacter.Should().NotBeNull();
            foundTaskCharacter.TaskId.Should().Be(defaultTaskId);
            foundTaskCharacter.CharacterId.Should().Be(defaultCharacterId);
        }
    }
}
