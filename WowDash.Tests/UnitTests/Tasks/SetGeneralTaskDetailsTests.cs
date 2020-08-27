using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowDash.ApplicationCore.Entities;
using WowDash.Tests.Common;
using WowDash.WebUI.Controllers;

namespace WowDash.Tests.UnitTests.Tasks
{
    [TestFixture]
    public class SetGeneralTaskDetailsTests : UnitTestBase
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

            var description = "Reach exalted reputation with The Defilers";
            var dto = new SetGeneralTaskDetailsRequest(task.Id, description);

            // Act
            var result = _controller.SetGeneralTaskDetails(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);
            foundTask.Description.Should().Be(description);
        }
    }
}
