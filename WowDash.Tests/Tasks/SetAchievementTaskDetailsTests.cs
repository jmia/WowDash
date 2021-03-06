﻿using FluentAssertions;
using NUnit.Framework;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class SetAchievementTaskDetailsTests : UnitTestBase
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

            var description = "Double Agent";
            var dto = new SetAchievementTaskDetailsRequest(task.Id, description, default, default);

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
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var priority = Priority.Highest;
            var dto = new SetAchievementTaskDetailsRequest(task.Id, null, default, priority);

            // Act
            var result = _controller.SetAchievementTaskDetails(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);
            foundTask.Priority.Should().Be(priority);
        }

        [Test]
        public void GivenAllValidValues_UpdatesTaskInDatabase()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            Context.Tasks.Add(task);
            Context.SaveChanges();

            var description = "Double Agent";
            var priority = Priority.Highest;
            var refresh = RefreshFrequency.Weekly;
            var dto = new SetAchievementTaskDetailsRequest(task.Id, description, refresh, priority);

            // Act
            var result = _controller.SetAchievementTaskDetails(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);
            foundTask.Description.Should().Be(description);
            foundTask.Priority.Should().Be(priority);
            foundTask.RefreshFrequency.Should().Be(refresh);
        }
    }
}
