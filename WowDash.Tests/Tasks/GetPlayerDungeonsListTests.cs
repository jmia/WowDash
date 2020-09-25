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
    public class GetPlayerDungeonsListTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenAMixedListOfTasks_ReturnsUniqueDungeonList()
        {
            // Arrange
            var karazhan = "Karazhan";
            var grimrailDepot = "Grimrail Depot";

            var firstTask = new Task(DefaultPlayer.Id, TaskType.General) {
                GameDataReferences = new List<GameDataReference>()
                { new GameDataReference(101, GameDataReference.GameDataType.Dungeon, null, karazhan) }
            };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.Collectible) {
                GameDataReferences = new List<GameDataReference>()
                { new GameDataReference(101, GameDataReference.GameDataType.Dungeon, null, karazhan) }
            };
            var thirdTask = new Task(DefaultPlayer.Id, TaskType.Collectible) {
                GameDataReferences = new List<GameDataReference>()
                { new GameDataReference(103, GameDataReference.GameDataType.Dungeon, null, grimrailDepot) }
            };

            Context.Tasks.AddRange(firstTask, secondTask, thirdTask);
            Context.SaveChanges();

            // Act
            var result = _controller.GetPlayerDungeonsList(DefaultPlayer.Id);

            // Assert
            Assert.IsInstanceOf<ICollection<FilterListSourceResponse>>(result.Value);
            result.Value.Count.Should().Be(2);
            result.Value.Any(d => d.Name.Equals(karazhan)).Should().BeTrue();
            result.Value.Any(d => d.Name.Equals(grimrailDepot)).Should().BeTrue();
        }

        [Test]
        public void GivenAListOfTasksWithoutDungeons_ReturnsEmptyList()
        {
            // Arrange

            var firstTask = new Task(DefaultPlayer.Id, TaskType.General)
            {
                GameDataReferences = new List<GameDataReference>()
                { new GameDataReference(101, GameDataReference.GameDataType.Zone, null, "Deadwind Pass") }
            };
            var secondTask = new Task(DefaultPlayer.Id, TaskType.Collectible);

            Context.Tasks.AddRange(firstTask, secondTask);
            Context.SaveChanges();

            // Act
            var result = _controller.GetPlayerDungeonsList(DefaultPlayer.Id);

            // Assert
            Assert.IsInstanceOf<ICollection<FilterListSourceResponse>>(result.Value);
            result.Value.Should().BeEmpty().And.HaveCount(0);
        }

    }
}
