using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class SetTaskCollectibleTypeAndSourceTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenAValidTask_AndValidProperties_UpdatesTaskInDatabase()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.Collectible);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var dto = new SetTaskCollectibleTypeAndSourceRequest(task.Id, CollectibleType.Item, Source.Dungeon);

            // Act
            var result = _controller.SetTaskCollectibleTypeAndSource(dto);

            var foundTask = Context.Tasks.Find(result.Value);

            // Assert
            foundTask.Should().NotBeNull();
            foundTask.CollectibleType.Should().Be(dto.CollectibleType);
            foundTask.Source.Should().Be(dto.Source);
        }

        [Test]
        public void GivenAnInvalidTask_ReturnsNotFound()
        {
            // Arrange
            var dto = new SetTaskCollectibleTypeAndSourceRequest(TestConstants.AllOnesGuid, CollectibleType.Item, Source.Dungeon);

            // Act
            var result = _controller.SetTaskCollectibleTypeAndSource(dto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }


    }
}
