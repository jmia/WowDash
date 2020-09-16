using FluentAssertions;
using NUnit.Framework;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class SetCollectibleTaskDetailsTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenAValidDescription_UpdatesTaskInDatabase()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var description = "Collect Invincible's Reins from The Lich King";
            var dto = new SetCollectibleTaskDetailsRequest(task.Id, description, default, default);

            // Act
            var result = _controller.SetCollectibleTaskDetails(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);
            foundTask.Description.Should().Be(description);
        }

        [Test]
        public void GivenAValidRefreshFrequency_UpdatesTaskInDatabase()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var refreshFrequency = RefreshFrequency.Weekly;
            var dto = new SetCollectibleTaskDetailsRequest(task.Id, null, refreshFrequency, default);

            // Act
            var result = _controller.SetCollectibleTaskDetails(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);
            foundTask.RefreshFrequency.Should().Be(refreshFrequency);
        }

        [Test]
        public void GivenAValidPriority_UpdatesTaskInDatabase()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var priority = Priority.Highest;
            var dto = new SetCollectibleTaskDetailsRequest(task.Id, null, default, priority);

            // Act
            var result = _controller.SetCollectibleTaskDetails(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);
            foundTask.Priority.Should().Be(priority);
        }
    }
}
