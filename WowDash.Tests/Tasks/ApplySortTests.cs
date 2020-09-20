using FluentAssertions;
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
    public class ApplySortTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenNoSortParam_SortsByPriorityDescending()
        {
            // Arrange
            var firstTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Zul'Gurub",
                Priority = Priority.High
            };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Arathi Basin",
                Priority = Priority.Low
            };
            var thirdTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Moonglade",
                Priority = Priority.Lowest
            };
            var fourthTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Netherstorm",
                Priority = Priority.Highest
            };

            Context.AddRange(firstTask, secondTask, thirdTask, fourthTask);
            Context.SaveChanges();

            var filterModel = new FilterModel();

            var taskList = Context.Tasks.Where(t => t.Id == firstTask.Id || t.Id == secondTask.Id || t.Id == thirdTask.Id || t.Id == fourthTask.Id);

            // Act
            _controller.ApplySort(ref taskList, filterModel);

            // Assert
            taskList.Should().BeInDescendingOrder(t => t.Priority);
        }

        [Test]
        public void GivenAnUnorderedList_SortsByAlphaAscending()
        {
            // Arrange
            var firstTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Zul'Gurub",
                Priority = Priority.High
            };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Arathi Basin",
                Priority = Priority.Low
            };
            var thirdTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Moonglade",
                Priority = Priority.Lowest
            };
            var fourthTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Netherstorm",
                Priority = Priority.Highest
            };

            Context.AddRange(firstTask, secondTask, thirdTask, fourthTask);
            Context.SaveChanges();

            var filterModel = new FilterModel() { SortBy = "alpha_asc" };

            var taskList = Context.Tasks.Where(t => t.Id == firstTask.Id || t.Id == secondTask.Id || t.Id == thirdTask.Id || t.Id == fourthTask.Id);

            // Act
            _controller.ApplySort(ref taskList, filterModel);

            // Assert
            taskList.Should().BeInAscendingOrder(t => t.Description);
        }

        [Test]
        public void GivenAnUnorderedList_SortsByAlphaDescending()
        {
            // Arrange
            var firstTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Zul'Gurub",
                Priority = Priority.High
            };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Arathi Basin",
                Priority = Priority.Low
            };
            var thirdTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Moonglade",
                Priority = Priority.Lowest
            };
            var fourthTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Netherstorm",
                Priority = Priority.Highest
            };

            Context.AddRange(firstTask, secondTask, thirdTask, fourthTask);
            Context.SaveChanges();

            var filterModel = new FilterModel() { SortBy = "alpha_desc" };

            var taskList = Context.Tasks.Where(t => t.Id == firstTask.Id || t.Id == secondTask.Id || t.Id == thirdTask.Id || t.Id == fourthTask.Id);

            // Act
            _controller.ApplySort(ref taskList, filterModel);

            // Assert
            taskList.Should().BeInDescendingOrder(t => t.Description);
        }

        [Test]
        public void GivenAnUnorderedList_SortsByPriorityAscending()
        {
            // Arrange
            var firstTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Zul'Gurub",
                Priority = Priority.High
            };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Arathi Basin",
                Priority = Priority.Low
            };
            var thirdTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Moonglade",
                Priority = Priority.Lowest
            };
            var fourthTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Netherstorm",
                Priority = Priority.Highest
            };

            Context.AddRange(firstTask, secondTask, thirdTask, fourthTask);
            Context.SaveChanges();

            var filterModel = new FilterModel() { SortBy = "priority_asc" };

            var taskList = Context.Tasks.Where(t => t.Id == firstTask.Id || t.Id == secondTask.Id || t.Id == thirdTask.Id || t.Id == fourthTask.Id);

            // Act
            _controller.ApplySort(ref taskList, filterModel);

            // Assert
            taskList.Should().BeInAscendingOrder(t => t.Priority);
        }

        [Test]
        public void GivenAnUnorderedList_SortsByPriorityDescending()
        {
            // Arrange
            var firstTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Zul'Gurub",
                Priority = Priority.High
            };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Arathi Basin",
                Priority = Priority.Low
            };
            var thirdTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Moonglade",
                Priority = Priority.Lowest
            };
            var fourthTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                Description = "Netherstorm",
                Priority = Priority.Highest
            };

            Context.AddRange(firstTask, secondTask, thirdTask, fourthTask);
            Context.SaveChanges();

            var filterModel = new FilterModel() { SortBy = "priority_desc" };

            var taskList = Context.Tasks.Where(t => t.Id == firstTask.Id || t.Id == secondTask.Id || t.Id == thirdTask.Id || t.Id == fourthTask.Id);

            // Act
            _controller.ApplySort(ref taskList, filterModel);

            // Assert
            taskList.Should().BeInDescendingOrder(t => t.Priority);
        }

    }
}
