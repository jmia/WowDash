using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
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
    public class AddTaskTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenAnInvalidUser_DoesNotAddTaskToDatabase()
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

        // Second biggest cop-out test of all time for the 
        // second biggest cop-out method of all time
        [Test]
        public void GivenValidUser_AndAllValidProperties_AddsTaskToDatabase()
        {
            // Arrange
            var dto = new AddTaskRequest()
            {
                PlayerId = DefaultPlayer.Id,
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
            var result = _controller.AddTask(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);

            foundTask.PlayerId.Should().Be(dto.PlayerId);
            foundTask.TaskType.Should().Be(dto.TaskType);
            foundTask.Description.Should().Be(dto.Description);
            foundTask.Notes.Should().Be(dto.Notes);
            foundTask.CollectibleType.Should().Be(dto.CollectibleType);
            foundTask.Source.Should().Be(dto.Source);
            foundTask.Priority.Should().Be(dto.Priority);
            foundTask.RefreshFrequency.Should().Be(dto.RefreshFrequency);
        }

        [Test]
        public void GivenAListOfCharacters_AddsTaskCharactersToDatabase()
        {
            // Arrange
            var dto = new AddTaskRequest()
            {
                PlayerId = DefaultPlayer.Id,
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

            var firstCharacter = new Character { PlayerId = DefaultPlayer.Id };
            var secondCharacter = new Character { PlayerId = DefaultPlayer.Id };

            Context.Characters.AddRange(firstCharacter, secondCharacter);

            Context.SaveChanges();

            dto.Characters.Add(firstCharacter.Id);
            dto.Characters.Add(secondCharacter.Id);

            // Act
            var result = _controller.AddTask(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);
            foundTask.TaskCharacters.Count.Should().Be(2);
        }
    }
}
