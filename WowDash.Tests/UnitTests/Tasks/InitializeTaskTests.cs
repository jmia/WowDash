using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using WowDash.ApplicationCore.DTO;
using WowDash.ApplicationCore.Entities;
using WowDash.Tests.Common;
using WowDash.WebUI.Controllers;

namespace WowDash.Tests.UnitTests.Tasks
{
    [TestFixture]
    public class InitializeTaskTests : UnitTestBase
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
        public void InitalizeTask_GivenValidUser_AddsTaskToDatabase()
        {
            // Arrange
            var dto = new InitializeTaskRequest(_defaultPlayer.Id);

            // Act
            var result = _controller.InitializeTask(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result);

            foundTask.PlayerId.Should().Be(dto.PlayerId);
        }

        [Test]
        public void InitializeTask_GivenAValidTaskType_AddsTaskToDatabase()
        {
            // Arrange
            var dto = new InitializeTaskRequest(_defaultPlayer.Id, TaskType.Collectible);

            // Act
            var result = _controller.InitializeTask(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result);

            foundTask.TaskType.Should().Be(dto.TaskType);
        }
    }
}
