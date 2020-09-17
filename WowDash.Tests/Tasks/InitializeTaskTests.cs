using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class InitializeTaskTests : UnitTestBase
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
            var dto = new InitializeTaskRequest(TestConstants.AllOnesGuid, default);

            // Act
            var result = _controller.InitializeTask(dto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);

        }

        [Test]
        public void GivenValidUser_AddsTaskToDatabase()
        {
            // Arrange
            var dto = new InitializeTaskRequest(DefaultPlayer.Id, default);

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
            var dto = new InitializeTaskRequest(DefaultPlayer.Id, TaskType.Collectible);

            // Act
            var result = _controller.InitializeTask(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);

            foundTask.TaskType.Should().Be(dto.TaskType);
        }
    }
}
