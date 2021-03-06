﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.TaskCharacters
{
    [TestFixture]
    public class SetAttemptCompleteTests : UnitTestBase
    {
        private TaskCharactersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TaskCharactersController(Context);
        }

        [Test]
        public void GivenAValidTaskCharacter_ShouldSetTaskCharacterToInactive()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            var character = new Character { PlayerId = DefaultPlayer.Id };

            Context.Tasks.Add(task);
            Context.Characters.Add(character);

            var taskCharacter = new TaskCharacter(character.Id, task.Id);

            Context.TaskCharacters.Add(taskCharacter);
            Context.SaveChanges();

            var dto = new SetAttemptCompleteRequest(taskCharacter.CharacterId, taskCharacter.TaskId);

            // Act
            _controller.SetAttemptComplete(dto);

            var foundTaskCharacter = Context.TaskCharacters.Find(dto.CharacterId, dto.TaskId);

            // Assert
            foundTaskCharacter.IsActive.Should().BeFalse();
        }

        [Test]
        public void GivenAnInvalidTaskId_ReturnsNotFound()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            var character = new Character { PlayerId = DefaultPlayer.Id };

            Context.Tasks.Add(task);
            Context.Characters.Add(character);

            var taskCharacter = new TaskCharacter(character.Id, task.Id);

            Context.TaskCharacters.Add(taskCharacter);
            Context.SaveChanges();

            var dto = new SetAttemptCompleteRequest(taskCharacter.CharacterId, TestConstants.AllOnesGuid);

            // Act
            var result = _controller.SetAttemptComplete(dto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void GivenAnInvalidCharacterId_ReturnsNotFound()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            var character = new Character { PlayerId = DefaultPlayer.Id };

            Context.Tasks.Add(task);
            Context.Characters.Add(character);

            var taskCharacter = new TaskCharacter(character.Id, task.Id);

            Context.TaskCharacters.Add(taskCharacter);
            Context.SaveChanges();

            var dto = new SetAttemptCompleteRequest(TestConstants.AllOnesGuid, taskCharacter.TaskId);

            // Act
            var result = _controller.SetAttemptComplete(dto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
