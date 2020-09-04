using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using WowDash.ApplicationCore.Entities;
using WowDash.Tests.Common;
using WowDash.WebUI.Controllers;
using WowDash.ApplicationCore.DTO;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.Tests.UnitTests.TaskCharacters
{
    [TestFixture]
    public class AddCharacterToTaskTests : UnitTestBase
    {
        private Player _defaultPlayer;
        private TaskCharactersController _controller;

        [SetUp]
        public void Setup()
        {
            Assume.That(Context.Players.Any(), "The testing database needs at least one user.");
            _defaultPlayer = Context.Players.First();
            _controller = new TaskCharactersController(Context);
        }

        [Test]
        public void GivenAValidCharacter_AddsTaskCharacterToDatabase()
        {
            // Arrange
            var character = new Character { PlayerId = _defaultPlayer.Id };
            var task = new Task(_defaultPlayer.Id, TaskType.General);

            Context.Characters.Add(character);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var dto = new AddCharacterToTaskRequest(character.Id, task.Id);

            // Act
            var result = _controller.AddCharacterToTask(dto);

            var foundTaskCharacter = Context.TaskCharacters.Find(result.Value.CharacterId, result.Value.TaskId);

            // Assert
            foundTaskCharacter.Should().NotBeNull();
        }

        [Test]
        public void GivenAValidCharacter_ShouldSetTaskCharacterIsActiveToTrue()
        {
            // Arrange
            var character = new Character { PlayerId = _defaultPlayer.Id };
            var task = new Task(_defaultPlayer.Id, TaskType.General);

            Context.Characters.Add(character);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var dto = new AddCharacterToTaskRequest(character.Id, task.Id);

            // Act
            var result = _controller.AddCharacterToTask(dto);

            var foundTaskCharacter = Context.TaskCharacters.Find(result.Value.CharacterId, result.Value.TaskId);

            // Assert
            foundTaskCharacter.IsActive.Should().BeTrue();
        }
    }
}
