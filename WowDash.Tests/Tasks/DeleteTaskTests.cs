using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class DeleteTaskTests : UnitTestBase
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
        public void GivenAValidTask_DeletesTaskFromDatabase()
        {
            // Arrange
            var task = new Task(_defaultPlayer.Id, TaskType.General);

            Context.Tasks.Add(task);
            Context.SaveChanges();

            // Act
            var result = _controller.DeleteTask(task.Id);

            var removedTask = Context.Tasks.Find(result.Value);

            // Assert
            removedTask.Should().BeNull();
        }

        [Test]
        public void GivenAValidTaskWithTaskCharacters_DeletesAllAssociatedTaskCharactersFromDatabase()
        {
            // Arrange
            var task = new Task(_defaultPlayer.Id, TaskType.General);

            var scully = Context.Characters.Where(c => c.Name.Equals("Scully")).FirstOrDefault();
            var chakwas = Context.Characters.Where(c => c.Name.Equals("Chakwas")).FirstOrDefault();

            Context.Tasks.Add(task);
            Context.SaveChanges();

            var taskCharacters = new List<TaskCharacter>
            {
                new TaskCharacter(scully.Id, task.Id),
                new TaskCharacter(chakwas.Id, task.Id),
            };

            Context.TaskCharacters.AddRange(taskCharacters);
            Context.SaveChanges();

            // Act
            var result = _controller.DeleteTask(task.Id);

            var removedTaskCharacters = Context.TaskCharacters.Where(tc => tc.TaskId == task.Id);

            // Assert
            removedTaskCharacters.Should().BeEmpty();
        }

        [Test]
        public void GivenAnInvalidTask_ReturnsNotFound()
        {
            // Arrange, Act
            var result = _controller.DeleteTask(TestConstants.AllOnesGuid);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
