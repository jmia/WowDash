using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class SetAchievementTaskDetailsTests : UnitTestBase
    {
        private Player _defaultPlayer;
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            Assume.That(Context.Players.Any(), "The testing database needs at least one user.");
            _defaultPlayer = Context.Players.First();
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenAValidDescription_UpdatesTaskInDatabase()
        {
            // Arrange
            var task = new Task(_defaultPlayer.Id, TaskType.General);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var description = "Double Agent";
            var dto = new SetAchievementTaskDetailsRequest(task.Id, description, default);

            // Act
            var result = _controller.SetAchievementTaskDetails(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);
            foundTask.Description.Should().Be(description);
        }

        [Test]
        public void GivenAValidPriority_UpdatesTaskInDatabase()
        {
            // Arrange
            var task = new Task(_defaultPlayer.Id, TaskType.General);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var priority = Priority.Highest;
            var dto = new SetAchievementTaskDetailsRequest(task.Id, null, priority);

            // Act
            var result = _controller.SetAchievementTaskDetails(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);
            foundTask.Priority.Should().Be(priority);
        }

        [Test]
        public void GivenAllValidValues_SetsDefaultRefreshPriority()
        {
            // Arrange
            var task = new Task(_defaultPlayer.Id, TaskType.General);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var description = "Double Agent";
            var priority = Priority.Highest;
            var dto = new SetAchievementTaskDetailsRequest(task.Id, description, priority);

            // Act
            var result = _controller.SetAchievementTaskDetails(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);
            foundTask.RefreshFrequency.Should().Be(RefreshFrequency.Never);
        }
    }
}
