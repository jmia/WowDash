﻿using FluentAssertions;
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
    public class SetTaskNotesTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenAValidTask_AndValidNotes_AddsNotes()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);
            var expectedNotes = "These are the expected notes.";

            Context.Tasks.Add(task);
            Context.SaveChanges();

            var dto = new SetTaskNotesRequest(task.Id, expectedNotes);

            // Act
            var result = _controller.SetTaskNotes(dto);

            var foundTask = Context.Tasks.Find(result.Value);

            // Assert
            foundTask.Notes.Should().Be(expectedNotes);
        }

        [Test]
        public void GivenAValidTaskWithExistingNotes_AndValidNotes_AddsNotes()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);

            task.Notes = "These are the original notes.";

            var expectedNotes = "These are the new notes.";

            Context.Tasks.Add(task);
            Context.SaveChanges();

            var dto = new SetTaskNotesRequest(task.Id, expectedNotes);

            // Act
            var result = _controller.SetTaskNotes(dto);

            var foundTask = Context.Tasks.Find(result.Value);

            // Assert
            foundTask.Notes.Should().Be(expectedNotes);
        }

        [Test]
        public void GivenAnInvalidTask_ReturnsNotFound()
        {
            // Arrange
            var dto = new SetTaskNotesRequest(TestConstants.AllOnesGuid, "Can't find this.");

            // Act
            var result = _controller.SetTaskNotes(dto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenAValidTask_AndNullOrEmptyNotes_SetsNotesToNull(string expectedNotes)
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);

            Context.Tasks.Add(task);
            Context.SaveChanges();

            var dto = new SetTaskNotesRequest(task.Id, expectedNotes);

            // Act
            var result = _controller.SetTaskNotes(dto);

            var foundTask = Context.Tasks.Find(result.Value);

            // Assert
            foundTask.Notes.Should().BeNull();
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GivenAValidTaskWithExistingNotes_AndNullOrEmptyNotes_RemovesNotes(string expectedNotes)
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.General);

            task.Notes = "I could be more boring than this, but I'm not going to try.";

            Context.Tasks.Add(task);
            Context.SaveChanges();

            var dto = new SetTaskNotesRequest(task.Id, expectedNotes);

            // Act
            var result = _controller.SetTaskNotes(dto);

            var foundTask = Context.Tasks.Find(result.Value);

            // Assert
            foundTask.Notes.Should().BeNull();
        }
    }
}
