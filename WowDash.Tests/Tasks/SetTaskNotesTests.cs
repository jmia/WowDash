using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Linq;
using WowDash.ApplicationCore.DTO;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class SetTaskNotesTests : UnitTestBase
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
        public void GivenAValidTask_AndValidNotes_AddsNotes()
        {
            // Arrange
            var task = new Task(_defaultPlayer.Id, TaskType.General);
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
            var task = new Task(_defaultPlayer.Id, TaskType.General);

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
            var task = new Task(_defaultPlayer.Id, TaskType.General);

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
            var task = new Task(_defaultPlayer.Id, TaskType.General);

            task.Notes = "This note should be removed.";

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
