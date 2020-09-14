using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Linq;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Characters
{
    public class DeleteCharacterTests : UnitTestBase
    {
        private Player _defaultPlayer;
        private CharactersController _controller;

        [SetUp]
        public void Setup()
        {
            Assume.That(Context.Players.Any(), "The testing database needs at least one user.");
            _defaultPlayer = Context.Players.First();
            _controller = new CharactersController(Context);
        }

        [Test]
        public void GivenAValidCharacter_RemovesCharacter()
        {
            // Arrange
            var character = new Character() { PlayerId = _defaultPlayer.Id };

            Context.Characters.Add(character);
            Context.SaveChanges();

            // Act
            var result = _controller.DeleteCharacter(character.Id);

            var foundCharacter = Context.Characters.Find(result.Value);

            // Assert
            foundCharacter.Should().BeNull();
        }

        [Test]
        public void GivenAValidCharacterWithTasks_RemovesTaskCharacters()
        {
            // Arrange
            var character = new Character() { PlayerId = _defaultPlayer.Id };
            var task = new Task(_defaultPlayer.Id, TaskType.General);

            Context.Characters.Add(character);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var taskCharacter = new TaskCharacter(character.Id, task.Id);

            Context.TaskCharacters.Add(taskCharacter);
            Context.SaveChanges();

            // Act
            var result = _controller.DeleteCharacter(character.Id);

            var foundTaskCharacters = Context.TaskCharacters.Where(tc => tc.CharacterId == result.Value);

            // Assert
            foundTaskCharacters.Should().BeEmpty();
        }

        [Test]
        public void GivenAnInvalidCharacterId_ReturnsNotFound()
        {
            // Arrange, Act
            var result = _controller.DeleteCharacter(TestConstants.AllOnesGuid);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
