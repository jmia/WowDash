using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WowDash.ApplicationCore.DTO.Responses;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class GetTaskByIdTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenAValidTaskId_ReturnsTask()
        {
            // Arrange
            var expectedTaskType = TaskType.Collectible;
            var expectedDescription = "Get Midnight's Eternal Reins from Karazhan";
            var expectedGdrDescription = "Midnight's Eternal Reins";
            var expectedGameDataReferences = new List<GameDataReference>()
            {
                new GameDataReference(142236, GameDataReference.GameDataType.Item, "Mount", expectedGdrDescription)
            };
            var expectedCollectibleType = CollectibleType.Mount;
            var expectedSource = Source.Dungeon;
            var expectedPriority = Priority.Medium;
            var expectedRefreshFrequency = RefreshFrequency.Weekly;

            var task = new Task(DefaultPlayer.Id, expectedTaskType)
            {
                Description = expectedDescription,
                GameDataReferences = expectedGameDataReferences,
                CollectibleType = expectedCollectibleType,
                Source = expectedSource,
                Priority = expectedPriority,
                RefreshFrequency = expectedRefreshFrequency
            };

            Context.Tasks.Add(task);
            Context.SaveChanges();

            // Act
            var result = _controller.GetTaskById(task.Id);

            // Assert
            Assert.IsInstanceOf<TaskResponse>(result.Value);

            result.Value.TaskId.Should().Be(task.Id);
            result.Value.TaskType.Should().Be(expectedTaskType);
            result.Value.Description.Should().Be(expectedDescription);
            result.Value.GameDataReferences.Should().NotBeEmpty().And.HaveCount(1);
            result.Value.GameDataReferences.Any(gdr => gdr.Description.Equals(expectedGdrDescription));
            result.Value.CollectibleType.Should().Be(expectedCollectibleType);
            result.Value.Source.Should().Be(expectedSource);
            result.Value.Priority.Should().Be(expectedPriority);
            result.Value.RefreshFrequency.Should().Be(expectedRefreshFrequency);
        }

        [Test]
        public void GivenAnInvalidTaskId_ReturnsNotFound()
        {
            // Arrange, Act
            var result = _controller.GetTaskById(TestConstants.AllOnesGuid);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
