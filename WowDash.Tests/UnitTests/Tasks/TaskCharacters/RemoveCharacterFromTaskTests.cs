using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using WowDash.ApplicationCore.Entities;
using WowDash.Tests.Common;
using WowDash.WebUI.Controllers;
using WowDash.ApplicationCore.DTO;
using static WowDash.ApplicationCore.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WowDash.Tests.UnitTests.Tasks.TaskCharacters
{
    [TestFixture]
    public class RemoveCharacterFromTaskTests : UnitTestBase
    {
        private Player _defaultPlayer;
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            Assume.That(Context.Players.Any(), "The testing database needs at least one user.");
            _defaultPlayer = Context.Players.First();
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenAValidCharacter_RemovesTaskCharacterFromDatabase()
        {
            // Arrange
            var task = new Task(_defaultPlayer.Id, TaskType.General);
            var character = new Character { PlayerId = _defaultPlayer.Id };
            var secondCharacter = new Character { PlayerId = _defaultPlayer.Id };

            Context.Tasks.Add(task);
            Context.Characters.AddRange(character, secondCharacter);

            var taskCharacter = new TaskCharacter(character.Id, task.Id);
            var secondTaskCharacter = new TaskCharacter(secondCharacter.Id, task.Id);

            Context.TaskCharacters.AddRange(taskCharacter, secondTaskCharacter);
            Context.SaveChanges();

            var dto = new RemoveCharacterFromTaskRequest(character.Id, task.Id);

            // Act
            var result = _controller.RemoveCharacterFromTask(dto);

            var foundTaskCharacters = Context.TaskCharacters.Where(tc => tc.TaskId == task.Id);

            // Assert
            foundTaskCharacters.Count().Should().Be(1);
            foundTaskCharacters.Should().NotContain(tc => tc.CharacterId == character.Id);
        }

        [Test]
        public void GivenAnInvalidCharacter_ReturnsNotFound()
        {
            // Arrange
            var task = new Task(_defaultPlayer.Id, TaskType.General);
            var character = new Character { PlayerId = _defaultPlayer.Id };

            Context.Tasks.Add(task);
            Context.Characters.Add(character);

            var taskCharacter = new TaskCharacter(character.Id, task.Id);

            Context.TaskCharacters.Add(taskCharacter);
            Context.SaveChanges();

            var dto = new RemoveCharacterFromTaskRequest(TestConstants.AllOnesGuid, task.Id);

            // Act
            var result = _controller.RemoveCharacterFromTask(dto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
