using FluentAssertions;
using NUnit.Framework;
using WowDash.ApplicationCore.DTO.Responses;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class GetFavouriteTasksTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenAValidPlayerId_ReturnsFavouriteTasks()
        {
            // Arrange
            var firstTask = new Task(DefaultPlayer.Id, TaskType.General) { IsFavourite = true };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.Collectible) { IsFavourite = false };

            Context.Tasks.AddRange(firstTask, secondTask);
            Context.SaveChanges();

            // Act
            var result = _controller.GetFavouriteTasks(DefaultPlayer.Id);

            // Assert
            Assert.IsInstanceOf<GetFavouriteTasksResponse>(result.Value);
            result.Value.Tasks.Count.Should().Be(1);
        }

        [Test]
        public void GivenAValidPlayer_WithNoFavourites_ReturnsEmptyList()
        {
            // Arrange
            var firstTask = new Task(DefaultPlayer.Id, TaskType.General) { IsFavourite = false };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.Collectible) { IsFavourite = false };

            Context.Tasks.AddRange(firstTask, secondTask);
            Context.SaveChanges();

            // Act
            var result = _controller.GetFavouriteTasks(DefaultPlayer.Id);

            // Assert
            Assert.IsInstanceOf<GetFavouriteTasksResponse>(result.Value);
            result.Value.Tasks.Count.Should().Be(0);
        }
    }
}
