using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WowDash.ApplicationCore.DTO.Common;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;
using static WowDash.ApplicationCore.Entities.GameDataReference;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class UpdateTaskTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenAnInvalidUser_DoesNotUpdateTask()
        {
            // Arrange
            var dto = new AddTaskRequest()
            {
                PlayerId = TestConstants.AllOnesGuid
            };

            // Act
            var result = _controller.AddTask(dto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);

        }

        // Biggest cop-out test of all time for the 
        // biggest cop-out method of all time
        [Test]
        public void GivenValidUser_AndAllValidProperties_UpdatesTaskInDatabase()
        {
            var task = new Task(DefaultPlayer.Id, TaskType.Achievement)
            {
                IsFavourite = false,
                Priority = Priority.Highest,
                Notes = "This shouldn't be here."
            };

            Context.Tasks.Add(task);
            Context.SaveChanges();

            // Arrange
            var dto = new UpdateTaskRequest()
            {
                TaskId = task.Id,
                TaskType = TaskType.Achievement,
                Description = "Secret Fish and Where To Find Them",
                Notes = null,
                CollectibleType = null,
                Source = null,
                Priority = Priority.Medium,
                RefreshFrequency = RefreshFrequency.Never
            };

            dto.GameDataReferenceItems.Add(
                new GameDataReferenceItem() 
                { 
                    Id = 2, 
                    GameId = 13502, 
                    Type = GameDataType.Achievement, 
                    Subclass = null, 
                    Description = "Secret Fish and Where To Find Them" 
                });

            // Act
            var result = _controller.UpdateTask(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);

            foundTask.PlayerId.Should().Be(DefaultPlayer.Id);
            foundTask.Id.Should().Be(dto.TaskId);
            foundTask.TaskType.Should().Be(dto.TaskType);
            foundTask.Description.Should().Be(dto.Description);
            foundTask.Notes.Should().BeNull();
            foundTask.CollectibleType.Should().BeNull();
            foundTask.Source.Should().BeNull();
            foundTask.Priority.Should().Be(dto.Priority);
            foundTask.RefreshFrequency.Should().Be(dto.RefreshFrequency);
        }

        [Test]
        public void GivenNoExistingTaskCharacters_AddsTaskCharacters()
        {
            var task = new Task(DefaultPlayer.Id, TaskType.Achievement);
            var firstCharacter = new Character();
            var secondCharacter = new Character();

            Context.Tasks.Add(task);
            Context.Characters.AddRange(firstCharacter, secondCharacter);
            Context.SaveChanges();

            // Arrange
            var dto = new UpdateTaskRequest()
            {
                TaskId = task.Id,
                Characters = new List<Guid>() { firstCharacter.Id, secondCharacter.Id }
            };

            // Act
            var result = _controller.UpdateTask(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);

            foundTask.Id.Should().Be(dto.TaskId);
            foundTask.TaskType.Should().Be(dto.TaskType);
            foundTask.TaskCharacters.Count.Should().Be(2);
            foundTask.TaskCharacters.Any(tc => tc.CharacterId == firstCharacter.Id).Should().BeTrue();
            foundTask.TaskCharacters.Any(tc => tc.CharacterId == secondCharacter.Id).Should().BeTrue();
        }

        [Test]
        public void GivenExistingTaskCharacters_AndMatchingIds_MakesNoChanges()
        {
            var task = new Task(DefaultPlayer.Id, TaskType.Achievement);
            var firstCharacter = new Character();
            var secondCharacter = new Character();

            Context.Tasks.Add(task);
            Context.Characters.AddRange(firstCharacter, secondCharacter);
            Context.SaveChanges();

            task.TaskCharacters.Add(new TaskCharacter(firstCharacter.Id, task.Id) { IsActive = false });
            task.TaskCharacters.Add(new TaskCharacter(secondCharacter.Id, task.Id) { IsActive = false });

            Context.SaveChanges();

            // Arrange
            var dto = new UpdateTaskRequest()
            {
                TaskId = task.Id,
                Characters = new List<Guid>() { firstCharacter.Id, secondCharacter.Id }
            };

            // Act
            var result = _controller.UpdateTask(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);

            foundTask.Id.Should().Be(dto.TaskId);
            foundTask.TaskType.Should().Be(dto.TaskType);
            foundTask.TaskCharacters.Count.Should().Be(2);
            foundTask.TaskCharacters.Where(tc => tc.CharacterId == firstCharacter.Id).FirstOrDefault().IsActive.Should().BeFalse();
            foundTask.TaskCharacters.Where(tc => tc.CharacterId == secondCharacter.Id).FirstOrDefault().IsActive.Should().BeFalse();
        }

        [Test]
        public void GivenExistingTaskCharacters_AndExtraExistingTaskCharacters_RemovesExtras()
        {
            var task = new Task(DefaultPlayer.Id, TaskType.Achievement);
            var firstCharacter = new Character();
            var secondCharacter = new Character();

            Context.Tasks.Add(task);
            Context.Characters.AddRange(firstCharacter, secondCharacter);
            Context.SaveChanges();

            task.TaskCharacters.Add(new TaskCharacter(firstCharacter.Id, task.Id) { IsActive = false });
            task.TaskCharacters.Add(new TaskCharacter(secondCharacter.Id, task.Id) { IsActive = false });

            Context.SaveChanges();

            // Arrange
            var dto = new UpdateTaskRequest()
            {
                TaskId = task.Id,
                Characters = new List<Guid>() { firstCharacter.Id }
            };

            // Act
            var result = _controller.UpdateTask(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);

            foundTask.Id.Should().Be(dto.TaskId);
            foundTask.TaskType.Should().Be(dto.TaskType);
            foundTask.TaskCharacters.Count.Should().Be(1);
            foundTask.TaskCharacters.Any(tc => tc.CharacterId == secondCharacter.Id).Should().BeFalse();
            foundTask.TaskCharacters.Where(tc => tc.CharacterId == firstCharacter.Id).FirstOrDefault().IsActive.Should().BeFalse();
        }

        [Test]
        public void GivenExistingTaskCharacters_AndExtraRequestCharacters_AddsRequestCharacters()
        {

        }

        [Test]
        public void GivenExistingTaskCharacters_AndExtraRequestCharacters_IgnoresExistingCharacters()
        {

        }


    }
}
