using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using WowDash.ApplicationCore.DTO;
using WowDash.ApplicationCore.Entities;
using WowDash.IntegrationTests;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class InitializeTaskTests : TestBase
    {

        // How do I get a DbContext? How do I get a controller?

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
        public void GivenValidUser_AddsTaskToDatabase()
        {
            // Arrange
            var dto = new InitializeTaskRequest(_defaultPlayer.Id, default);

            // Act
            var result = _controller.InitializeTask(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);

            foundTask.PlayerId.Should().Be(dto.PlayerId);
        }

        [Test]
        public void GivenAValidTaskType_AddsTaskToDatabase()
        {
            // Arrange
            var dto = new InitializeTaskRequest(_defaultPlayer.Id, TaskType.Collectible);

            // Act
            var result = _controller.InitializeTask(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);

            foundTask.TaskType.Should().Be(dto.TaskType);
        }
    }
}
