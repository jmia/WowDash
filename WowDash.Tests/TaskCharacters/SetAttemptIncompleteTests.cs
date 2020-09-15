using FluentAssertions;
using NUnit.Framework;
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
        private TaskCharactersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TaskCharactersController(Context);
        }

        [Test]
        public void GivenAValidTaskCharacter_ShouldSetTaskCharacterToActive()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            var character = new Character { PlayerId = DefaultPlayer.Id };

            Context.Tasks.Add(task);
            Context.Characters.Add(character);

            var taskCharacter = new TaskCharacter(character.Id, task.Id);

            Context.TaskCharacters.Add(taskCharacter);
            Context.SaveChanges();

            var request = new SetAttemptIncompleteRequest(taskCharacter.CharacterId, taskCharacter.TaskId);

            // Act
            _controller.SetAttemptIncomplete(request);

            var foundTaskCharacter = Context.TaskCharacters.Find(request.CharacterId, request.TaskId);

            // Assert
            foundTaskCharacter.IsActive.Should().BeTrue();
        }
    }
}
