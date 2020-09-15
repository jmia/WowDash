using FluentAssertions;
using NUnit.Framework;
using WowDash.ApplicationCore.Entities;
using WowDash.WebUI.Controllers;
using WowDash.ApplicationCore.DTO;
using static WowDash.ApplicationCore.Common.Enums;
using WowDash.UnitTests.Common;

namespace WowDash.UnitTests.TaskCharacters
{
    [TestFixture]
    public class AddCharacterToTaskTests : UnitTestBase
    {
        private TaskCharactersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TaskCharactersController(Context);
        }

        [Test]
        public void GivenAValidCharacter_AddsTaskCharacterToDatabase()
        {
            // Arrange
            var character = new Character { PlayerId = DefaultPlayer.Id };
            var task = new Task(DefaultPlayer.Id, TaskType.General);

            Context.Characters.Add(character);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var dto = new AddCharacterToTaskRequest(character.Id, task.Id);

            // Act
            _controller.AddCharacterToTask(dto);

            var foundTaskCharacter = Context.TaskCharacters.Find(dto.CharacterId, dto.TaskId);

            // Assert
            foundTaskCharacter.Should().NotBeNull();
        }

        [Test]
        public void GivenAValidCharacter_ShouldSetTaskCharacterIsActiveToTrue()
        {
            // Arrange
            var character = new Character { PlayerId = DefaultPlayer.Id };
            var task = new Task(DefaultPlayer.Id, TaskType.General);

            Context.Characters.Add(character);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var dto = new AddCharacterToTaskRequest(character.Id, task.Id);

            // Act
            _controller.AddCharacterToTask(dto);

            var foundTaskCharacter = Context.TaskCharacters.Find(dto.CharacterId, dto.TaskId);

            // Assert
            foundTaskCharacter.IsActive.Should().BeTrue();
        }
    }
}
