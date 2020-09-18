using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WowDash.ApplicationCore.DTO.Responses;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.TaskCharacters
{
    [TestFixture]
    public class GetTaskCharacterByIdTests : UnitTestBase
    {
        private TaskCharactersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TaskCharactersController(Context);
        }

        [Test]
        public void GivenAValidCompositeKey_ReturnsTaskCharacter()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            var character = new Character(DefaultPlayer.Id, null, "Stella", CharacterGender.Female, null, "Paladin", "Blood Elf", "magtheridon");

            Context.Tasks.Add(task);
            Context.Characters.Add(character);
            Context.SaveChanges();

            var taskCharacter = new TaskCharacter(character.Id, task.Id);

            Context.TaskCharacters.Add(taskCharacter);
            Context.SaveChanges();

            // Act
            var result = _controller.GetTaskCharacterById(character.Id, task.Id);

            var foundTaskCharacter = Context.TaskCharacters.Find(result.Value.CharacterId, result.Value.TaskId);

            // Assert
            Assert.IsInstanceOf<GetTaskCharacterByIdResponse>(result.Value);

            foundTaskCharacter.Should().NotBeNull();
            foundTaskCharacter.TaskId.Should().Be(task.Id);
            foundTaskCharacter.CharacterId.Should().Be(character.Id);
            foundTaskCharacter.IsActive.Should().BeTrue();
        }

        [Test]
        public void GivenAnInvalidTaskId_ReturnsNotFound()
        {
            // Arrange
            var character = new Character(DefaultPlayer.Id, null, "Stella", CharacterGender.Female, null, "Paladin", "Blood Elf", "magtheridon");

            Context.Characters.Add(character);
            Context.SaveChanges();

            // Act
            var result = _controller.GetTaskCharacterById(character.Id, TestConstants.AllOnesGuid);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void GivenAnInvalidCharacterId_ReturnsNotFound()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);

            Context.Tasks.Add(task);
            Context.SaveChanges();

            // Act
            var result = _controller.GetTaskCharacterById(TestConstants.AllOnesGuid, task.Id);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
