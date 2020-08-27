using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using WowDash.ApplicationCore.Entities;
using WowDash.Tests.Common;
using WowDash.WebUI.Controllers;

namespace WowDash.Tests.UnitTests.Tasks
{
    [TestFixture]
    public class TaskTests : UnitTestBase
    {
        private Player _defaultPlayer;

        [SetUp]
        public void Setup()
        {
            Assume.That(Context.Players.Any(), "The testing database needs at least one user.");
            _defaultPlayer = Context.Players.First();
        }

        [Test]
        public void InitalizeTask_GivenValidUser_AddsTaskToDatabase()
        {
            // Arrange
            var controller = new TasksController(Context);

            var task = new Task(_defaultPlayer.Id);

            // Act
            controller.InitializeTask(task);

            // Assert
            var foundTask = Context.Tasks.Find(task.Id);

            foundTask.PlayerId.Should().Be(task.PlayerId);
        }
    }
}
