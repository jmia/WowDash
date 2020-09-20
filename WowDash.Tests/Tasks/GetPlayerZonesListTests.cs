using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WowDash.ApplicationCore.DTO.Common;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class GetPlayerZonesListTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenAMixedListOfTasks_ReturnsUniqueZoneList()
        {
            // Arrange
            var deadwindPass = "Deadwind Pass";
            var nagrand = "Nagrand";

            var firstTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                GameDataReferences = new List<GameDataReference>()
                { new GameDataReference(101, GameDataReference.GameDataType.QuestArea, null, deadwindPass) }
            };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.Collectible)
            {
                GameDataReferences = new List<GameDataReference>()
                { new GameDataReference(101, GameDataReference.GameDataType.QuestArea, null, deadwindPass) }
            };
            var thirdTask = new Task(DefaultPlayer.Id, TaskType.Collectible)
            {
                GameDataReferences = new List<GameDataReference>()
                { new GameDataReference(103, GameDataReference.GameDataType.QuestArea, null, nagrand) }
            };

            Context.Tasks.AddRange(firstTask, secondTask, thirdTask);
            Context.SaveChanges();

            // Act
            var result = _controller.GetPlayerZonesList(DefaultPlayer.Id);

            // Assert
            Assert.IsInstanceOf<ICollection<FilterListSourceResponse>>(result.Value);
            result.Value.Count.Should().Be(2);
            result.Value.Any(d => d.Name.Equals(deadwindPass)).Should().BeTrue();
            result.Value.Any(d => d.Name.Equals(nagrand)).Should().BeTrue();
        }

        [Test]
        public void GivenAListOfTasksWithoutZones_ReturnsEmptyList()
        {
            // Arrange

            var firstTask = new Task(DefaultPlayer.Id, TaskType.General)
            { 
                GameDataReferences = new List<GameDataReference>()
                { new GameDataReference(101, GameDataReference.GameDataType.JournalInstance, null, "Karazhan") }
            };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.Collectible);

            Context.Tasks.AddRange(firstTask, secondTask);
            Context.SaveChanges();

            // Act
            var result = _controller.GetPlayerZonesList(DefaultPlayer.Id);

            // Assert
            Assert.IsInstanceOf<ICollection<FilterListSourceResponse>>(result.Value);
            result.Value.Should().BeEmpty().And.HaveCount(0);
        }
    }
}
