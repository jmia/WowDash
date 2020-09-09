using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.TaskCharacters
{
    [TestFixture]
    public class RefreshDailyTaskCharactersTests : UnitTestBase
    {
        private Player _defaultPlayer;
        private TaskCharactersController _controller;

        [SetUp]
        public void Setup()
        {
            Assume.That(Context.Players.Any(), "The testing database needs at least one user.");
            Assume.That(Context.Characters.Count() > 1, "The testing database needs two characters.");
            _defaultPlayer = Context.Players.First();
            _controller = new TaskCharactersController(Context);
        }

        [Test]
        public void GivenACollectionOfDailyRecurringTasks_SetsAllTaskCharactersToActive()
        {
            // Arrange
            var firstTask = new Task(_defaultPlayer.Id, TaskType.General);
            var secondTask = new Task(_defaultPlayer.Id, TaskType.General);
            var thirdTask = new Task(_defaultPlayer.Id, TaskType.General);

            firstTask.RefreshFrequency = RefreshFrequency.Daily;
            secondTask.RefreshFrequency = RefreshFrequency.Daily;
            thirdTask.RefreshFrequency = RefreshFrequency.Daily;

            var scully = Context.Characters.Where(c => c.Name.Equals("Scully")).FirstOrDefault();
            var chakwas = Context.Characters.Where(c => c.Name.Equals("Chakwas")).FirstOrDefault();

            Context.Tasks.AddRange(firstTask, secondTask, thirdTask);
            Context.SaveChanges();

            var taskCharacters = new List<TaskCharacter>
            {
                new TaskCharacter(scully.Id, firstTask.Id),
                new TaskCharacter(chakwas.Id, firstTask.Id),
                new TaskCharacter(scully.Id, secondTask.Id),
                new TaskCharacter(chakwas.Id, secondTask.Id),
                new TaskCharacter(scully.Id, thirdTask.Id),
                new TaskCharacter(chakwas.Id, thirdTask.Id)
            };

            foreach (var tc in taskCharacters)
            {
                tc.IsActive = false;
            }

            Context.TaskCharacters.AddRange(taskCharacters);
            Context.SaveChanges();

            // Act
            var result = _controller.RefreshDailyTaskCharacters();

            var foundTaskCharacters = Context.TaskCharacters.Where(
                tc => tc.TaskId == firstTask.Id ||
                tc.TaskId == secondTask.Id ||
                tc.TaskId == thirdTask.Id);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            foundTaskCharacters.All(tc => tc.IsActive == true);
        }

        [Test]
        public void GivenACollectionOfSomeDailyRecurringTasks_SetsOnlyDailyTaskCharactersToActive()
        {
            // Arrange
            var firstTask = new Task(_defaultPlayer.Id, TaskType.General);
            var secondTask = new Task(_defaultPlayer.Id, TaskType.General);
            var thirdTask = new Task(_defaultPlayer.Id, TaskType.General);

            firstTask.RefreshFrequency = RefreshFrequency.Daily;
            secondTask.RefreshFrequency = RefreshFrequency.Daily;
            thirdTask.RefreshFrequency = RefreshFrequency.Weekly;

            var scully = Context.Characters.Where(c => c.Name.Equals("Scully")).FirstOrDefault();
            var chakwas = Context.Characters.Where(c => c.Name.Equals("Chakwas")).FirstOrDefault();

            Context.Tasks.AddRange(firstTask, secondTask, thirdTask);
            Context.SaveChanges();

            var taskCharacters = new List<TaskCharacter>
            {
                new TaskCharacter(scully.Id, firstTask.Id),
                new TaskCharacter(chakwas.Id, firstTask.Id),
                new TaskCharacter(scully.Id, secondTask.Id),
                new TaskCharacter(chakwas.Id, secondTask.Id),
                new TaskCharacter(scully.Id, thirdTask.Id),
                new TaskCharacter(chakwas.Id, thirdTask.Id)
            };

            foreach (var tc in taskCharacters)
            {
                tc.IsActive = false;
            }

            Context.TaskCharacters.AddRange(taskCharacters);
            Context.SaveChanges();

            // Act
            var result = _controller.RefreshDailyTaskCharacters();

            var dailyTaskCharacters = Context.TaskCharacters.Where(
                tc => tc.TaskId == firstTask.Id ||
                tc.TaskId == secondTask.Id);
            var weeklyTaskCharacters = Context.TaskCharacters.Where(
                tc => tc.TaskId == thirdTask.Id);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            dailyTaskCharacters.All(tc => tc.IsActive == true);
            weeklyTaskCharacters.All(tc => tc.IsActive == false);
        }

        [Test]
        public void GivenACollectionWithNoDailyRecurringTasks_SetsNoTaskCharactersToActive()
        {
            // Arrange
            var firstTask = new Task(_defaultPlayer.Id, TaskType.General);
            var secondTask = new Task(_defaultPlayer.Id, TaskType.General);
            var thirdTask = new Task(_defaultPlayer.Id, TaskType.General);

            firstTask.RefreshFrequency = RefreshFrequency.Weekly;
            secondTask.RefreshFrequency = RefreshFrequency.Weekly;
            thirdTask.RefreshFrequency = RefreshFrequency.Never;

            var scully = Context.Characters.Where(c => c.Name.Equals("Scully")).FirstOrDefault();
            var chakwas = Context.Characters.Where(c => c.Name.Equals("Chakwas")).FirstOrDefault();

            Context.Tasks.AddRange(firstTask, secondTask, thirdTask);
            Context.SaveChanges();

            var taskCharacters = new List<TaskCharacter>
            {
                new TaskCharacter(scully.Id, firstTask.Id),
                new TaskCharacter(chakwas.Id, firstTask.Id),
                new TaskCharacter(scully.Id, secondTask.Id),
                new TaskCharacter(chakwas.Id, secondTask.Id),
                new TaskCharacter(scully.Id, thirdTask.Id),
                new TaskCharacter(chakwas.Id, thirdTask.Id)
            };

            foreach (var tc in taskCharacters)
            {
                tc.IsActive = false;
            }

            Context.TaskCharacters.AddRange(taskCharacters);
            Context.SaveChanges();

            // Act
            var result = _controller.RefreshDailyTaskCharacters();

            var foundTaskCharacters = Context.TaskCharacters.Where(
                tc => tc.TaskId == firstTask.Id ||
                tc.TaskId == secondTask.Id ||
                tc.TaskId == thirdTask.Id);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
            foundTaskCharacters.All(tc => tc.IsActive == false);
        }
    }
}
