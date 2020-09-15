using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WowDash.ApplicationCore.DTO;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class AddTaskToFavouritesTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenValidTask_AddsTaskToFavourites()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var dto = new AddTaskToFavouritesRequest(task.Id);

            // Act
            var result = _controller.AddTaskToFavourites(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);
            foundTask.IsFavourite.Should().BeTrue();
        }

        [Test]
        public void GivenAnInvalidTask_ReturnsNotFound()
        {
            // Arrange
            var dto = new AddTaskToFavouritesRequest(TestConstants.AllOnesGuid);

            // Act
            var result = _controller.AddTaskToFavourites(dto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
