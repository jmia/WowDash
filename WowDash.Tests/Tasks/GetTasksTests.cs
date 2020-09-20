using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;
using WowDash.ApplicationCore.DTO.Responses;
using WowDash.ApplicationCore.DTO.Common;
using System.Linq;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class GetTasksTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [TearDown]
        public void TearDown()
        {
            var tasks = Context.Tasks.Where(t => t.PlayerId == DefaultPlayer.Id);
            Context.Tasks.RemoveRange(tasks);
            Context.SaveChanges();
        }

        [Test]
        public void GivenAValidPlayerId_GetsAllTasks()
        {
            // Arrange
            var firstTask = new Task(DefaultPlayer.Id, TaskType.General) { IsFavourite = true };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.Collectible) { IsFavourite = false };

            Context.Tasks.AddRange(firstTask, secondTask);
            Context.SaveChanges();

            var filterModel = new FilterModel(DefaultPlayer.Id);

            // Act
            var result = _controller.GetTasks(filterModel);

            // Assert
            Assert.IsInstanceOf<GetTasksResponse>(result.Value);
            result.Value.Tasks.Count.Should().Be(2);
        }

        [Test]
        public void GivenACharacterId_GetsAllTasksAssignedToCharacter()
        {
            // Arrange
            var firstTask = new Task(DefaultPlayer.Id, TaskType.General) { IsFavourite = true };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.Collectible) { IsFavourite = false };

            Context.Tasks.AddRange(firstTask, secondTask);
            Context.SaveChanges();

            // TODO: Add TaskCharacters

            //// Act
            //var result = _controller.GetTasks();

            //// Assert
            //Assert.IsInstanceOf<GetTasksResponse>(result.Value);
            //result.Value.Tasks.Count.Should().Be(2);
        }
    }
}
