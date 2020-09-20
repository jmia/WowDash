﻿using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowDash.ApplicationCore.DTO.Common;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class ApplyFiltersTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenNoParameters_DoesNotFilterList()
        {

        }

        [Test]
        public void GivenNoTaskTypes_DoesNotFilterList()
        {

        }

        [Test]
        public void GivenAListOfCharacterIds_FiltersListByCharacters()
        {
            // Arrange
            var firstTask = new Task(DefaultPlayer.Id, TaskType.General);
            var secondTask = new Task(DefaultPlayer.Id, TaskType.General);
            var thirdTask = new Task(DefaultPlayer.Id, TaskType.General);
            var fourthTask = new Task(DefaultPlayer.Id, TaskType.General);

            var scully = Context.Characters.Where(c => c.Name.Equals("Scully")).FirstOrDefault();
            var chakwas = Context.Characters.Where(c => c.Name.Equals("Chakwas")).FirstOrDefault();

            var temperance = new Character() { Name = "Temperance" };

            Context.Characters.Add(temperance);
            Context.Tasks.AddRange(firstTask, secondTask, thirdTask);
            Context.SaveChanges();

            var taskCharacters = new List<TaskCharacter>
            {
                new TaskCharacter(scully.Id, firstTask.Id),
                new TaskCharacter(chakwas.Id, firstTask.Id),
                new TaskCharacter(scully.Id, secondTask.Id),
                new TaskCharacter(chakwas.Id, secondTask.Id),
                new TaskCharacter(temperance.Id, secondTask.Id),
                new TaskCharacter(scully.Id, thirdTask.Id),
                new TaskCharacter(temperance.Id, thirdTask.Id),
                new TaskCharacter(scully.Id, fourthTask.Id),
            };

            Context.TaskCharacters.AddRange(taskCharacters);
            Context.SaveChanges();

            var taskList = Context.Tasks.Where(t => t.Id == firstTask.Id || t.Id == secondTask.Id || t.Id == thirdTask.Id || t.Id == fourthTask.Id);

            var filterModel = new FilterModel
            {
                CharacterId = $"{chakwas.Id}|{temperance.Id}"
            };

            // Act
            _controller.ApplyFilters(ref taskList, filterModel);

            // Assert
            taskList.Should().HaveCount(3);
            taskList.Any(t => t.Id == firstTask.Id).Should().BeTrue();
            taskList.Any(t => t.Id == secondTask.Id).Should().BeTrue();
            taskList.Any(t => t.Id == thirdTask.Id).Should().BeTrue();
        }

        [TestCase(null, 3)]
        [TestCase("0", 1)]
        [TestCase("1", 1)]
        [TestCase("2", 1)]
        [TestCase("0|1", 2)]
        [TestCase("0|2", 2)]
        [TestCase("1|2", 2)]
        [TestCase("1|0", 2)]
        [TestCase("2|0", 2)]
        [TestCase("2|1", 2)]
        [TestCase("0|1|2", 3)]
        [TestCase("0|2|1", 3)]
        [TestCase("1|0|2", 3)]
        [TestCase("1|2|0", 3)]
        [TestCase("2|0|1", 3)]
        [TestCase("2|1|0", 3)]
        public void GivenAnyTaskTypes_FiltersList(string taskTypes, int count)
        {
            // Arrange
            var firstTask = new Task(DefaultPlayer.Id, TaskType.General);
            var secondTask = new Task(DefaultPlayer.Id, TaskType.Collectible);
            var thirdTask = new Task(DefaultPlayer.Id, TaskType.Achievement);

            Context.Tasks.AddRange(firstTask, secondTask, thirdTask);
            Context.SaveChanges();

            var tasks = Context.Tasks.Where(t => t.Id == firstTask.Id || t.Id == secondTask.Id || t.Id == thirdTask.Id);

            var filterModel = new FilterModel(DefaultPlayer.Id) { TaskType = taskTypes };

            // Act
            _controller.ApplyFilters(ref tasks, filterModel);

            // Assert
            tasks.Count().Should().Be(count);
        }

        [TestCase(null, 3)]
        [TestCase("0", 1)]
        [TestCase("1", 1)]
        [TestCase("2", 1)]
        [TestCase("0|1", 2)]
        [TestCase("0|2", 2)]
        [TestCase("1|2", 2)]
        [TestCase("1|0", 2)]
        [TestCase("2|0", 2)]
        [TestCase("2|1", 2)]
        [TestCase("0|1|2", 3)]
        [TestCase("0|2|1", 3)]
        [TestCase("1|0|2", 3)]
        [TestCase("1|2|0", 3)]
        [TestCase("2|0|1", 3)]
        [TestCase("2|1|0", 3)]
        public void GivenAnyRefreshFrequencies_FiltersList(string refreshFrequencies, int count)
        {
            // Arrange
            var firstTask = new Task(DefaultPlayer.Id, TaskType.General) { RefreshFrequency = RefreshFrequency.Never};
            var secondTask = new Task(DefaultPlayer.Id, TaskType.Collectible) { RefreshFrequency = RefreshFrequency.Daily };
            var thirdTask = new Task(DefaultPlayer.Id, TaskType.Achievement) { RefreshFrequency = RefreshFrequency.Weekly };

            Context.Tasks.AddRange(firstTask, secondTask, thirdTask);
            Context.SaveChanges();

            var tasks = Context.Tasks.Where(t => t.Id == firstTask.Id || t.Id == secondTask.Id || t.Id == thirdTask.Id);

            var filterModel = new FilterModel(DefaultPlayer.Id) { RefreshFrequency = refreshFrequencies };

            // Act
            _controller.ApplyFilters(ref tasks, filterModel);

            // Assert
            tasks.Count().Should().Be(count);
        }

        [TestCase(null, 4)]
        [TestCase("0", 1)]
        [TestCase("1", 1)]
        [TestCase("2", 1)]
        [TestCase("3", 1)]
        [TestCase("0|1", 2)]
        [TestCase("0|2", 2)]
        [TestCase("0|3", 2)]
        [TestCase("1|0", 2)]
        [TestCase("1|2", 2)]
        [TestCase("1|3", 2)]
        [TestCase("2|0", 2)]
        [TestCase("2|1", 2)]
        [TestCase("2|3", 2)]
        [TestCase("3|0", 2)]
        [TestCase("3|1", 2)]
        [TestCase("3|2", 2)]
        [TestCase("0|1|2", 3)]
        [TestCase("0|1|3", 3)]
        [TestCase("0|2|1", 3)]
        [TestCase("0|2|3", 3)]
        [TestCase("0|3|1", 3)]
        [TestCase("0|3|2", 3)]
        [TestCase("1|0|2", 3)]
        [TestCase("1|0|3", 3)]
        [TestCase("1|2|0", 3)]
        [TestCase("1|2|3", 3)]
        [TestCase("2|0|1", 3)]
        [TestCase("2|0|3", 3)]
        [TestCase("2|1|0", 3)]
        [TestCase("2|1|3", 3)]
        [TestCase("0|1|2|3", 4)]
        [TestCase("0|1|3|2", 4)]
        [TestCase("0|2|1|3", 4)]
        [TestCase("0|2|3|1", 4)]
        [TestCase("0|3|2|1", 4)]
        [TestCase("0|3|1|2", 4)]
        [TestCase("1|0|2|3", 4)]
        [TestCase("1|0|3|2", 4)]
        [TestCase("1|2|0|3", 4)]
        [TestCase("1|2|3|0", 4)]
        [TestCase("1|3|2|0", 4)]
        [TestCase("1|3|0|2", 4)]
        [TestCase("2|0|1|3", 4)]
        [TestCase("2|0|3|1", 4)]
        [TestCase("2|1|0|3", 4)]
        [TestCase("2|1|3|0", 4)]
        [TestCase("2|3|0|1", 4)]
        [TestCase("2|3|1|0", 4)]
        [TestCase("3|0|1|2", 4)]
        [TestCase("3|0|2|1", 4)]
        [TestCase("3|1|0|2", 4)]
        [TestCase("3|1|2|0", 4)]
        [TestCase("3|2|0|1", 4)]
        [TestCase("3|2|1|0", 4)]    // Math, y'all.
        public void GivenAnyCollectibleTypes_FiltersList(string collectibleTypes, int count)
        {
            // Arrange
            var firstTask = new Task(DefaultPlayer.Id, TaskType.Collectible) { CollectibleType = CollectibleType.Item };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.Collectible) { CollectibleType = CollectibleType.ItemSet };
            var thirdTask = new Task(DefaultPlayer.Id, TaskType.Collectible) { CollectibleType = CollectibleType.Mount };
            var fourthTask = new Task(DefaultPlayer.Id, TaskType.Collectible) { CollectibleType = CollectibleType.Pet };

            Context.Tasks.AddRange(firstTask, secondTask, thirdTask, fourthTask);
            Context.SaveChanges();

            var tasks = Context.Tasks.Where(t => t.Id == firstTask.Id || 
                t.Id == secondTask.Id || 
                t.Id == thirdTask.Id || 
                t.Id == fourthTask.Id);

            var filterModel = new FilterModel(DefaultPlayer.Id) { CollectibleType = collectibleTypes };

            // Act
            _controller.ApplyFilters(ref tasks, filterModel);

            // Assert
            tasks.Count().Should().Be(count);
        }

    }
}
