using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using WowDash.ApplicationCore.DTO;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.TaskCharacters
{
    [TestFixture]
    public class SetAttemptIncompleteTests : UnitTestBase
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
        public void GivenAValidTaskCharacter_ShouldSetTaskCharacterToActive()
        {
            // Arrange
            var task = new Task(_defaultPlayer.Id, TaskType.General);
            var character = new Character { PlayerId = _defaultPlayer.Id };

            Context.Tasks.Add(task);
            Context.Characters.Add(character);

            var taskCharacter = new TaskCharacter(character.Id, task.Id);

            Context.TaskCharacters.Add(taskCharacter);
            Context.SaveChanges();

            var request = new SetAttemptIncompleteRequest(taskCharacter.CharacterId, taskCharacter.TaskId);

            // Act
            var result = _controller.SetAttemptIncomplete(request);

            var foundTaskCharacter = Context.TaskCharacters.Find(result.Value.CharacterId, result.Value.TaskId);

            // Assert
            foundTaskCharacter.IsActive.Should().Be(true);
        }
    }
}
