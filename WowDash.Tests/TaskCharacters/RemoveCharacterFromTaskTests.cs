using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using WowDash.ApplicationCore.Entities;
using WowDash.WebUI.Controllers;
using WowDash.ApplicationCore.DTO;
using static WowDash.ApplicationCore.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using WowDash.UnitTests.Common;

namespace WowDash.UnitTests.TaskCharacters
{
    [TestFixture]
    public class RemoveCharacterFromTaskTests : UnitTestBase
    {
        private TaskCharactersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TaskCharactersController(Context);
        }

        [Test]
        public void GivenATaskWithTwoTaskCharacters_RemovesOnlyOneTaskCharacterFromDatabase()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            var character = new Character { PlayerId = DefaultPlayer.Id };
            var secondCharacter = new Character { PlayerId = DefaultPlayer.Id };

            Context.Tasks.Add(task);
            Context.Characters.AddRange(character, secondCharacter);

            var taskCharacter = new TaskCharacter(character.Id, task.Id);
            var secondTaskCharacter = new TaskCharacter(secondCharacter.Id, task.Id);

            Context.TaskCharacters.AddRange(taskCharacter, secondTaskCharacter);
            Context.SaveChanges();

            // Act
            _controller.RemoveCharacterFromTask(character.Id, task.Id);

            var foundTaskCharacters = Context.TaskCharacters.Where(tc => tc.TaskId == task.Id);

            // Assert
            foundTaskCharacters.Count().Should().Be(1);
            foundTaskCharacters.Should().NotContain(tc => tc.CharacterId == character.Id);
        }

        [Test]
        public void GivenATaskWithAValidCharacter_RemovesTaskCharacterFromDatabase()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            var character = new Character { PlayerId = DefaultPlayer.Id };

            Context.Tasks.Add(task);
            Context.Characters.Add(character);

            var taskCharacter = new TaskCharacter(character.Id, task.Id);

            Context.TaskCharacters.Add(taskCharacter);
            Context.SaveChanges();

            // Act
            _controller.RemoveCharacterFromTask(character.Id, task.Id);

            var foundTaskCharacters = Context.TaskCharacters.Where(tc => tc.TaskId == task.Id);

            // Assert
            foundTaskCharacters.Should().BeEmpty();
        }

        [Test]
        public void GivenAnInvalidCharacter_ReturnsNotFound()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            var character = new Character { PlayerId = DefaultPlayer.Id };

            Context.Tasks.Add(task);
            Context.Characters.Add(character);

            var taskCharacter = new TaskCharacter(character.Id, task.Id);

            Context.TaskCharacters.Add(taskCharacter);
            Context.SaveChanges();

            // Act
            var result = _controller.RemoveCharacterFromTask(TestConstants.AllOnesGuid, task.Id);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
