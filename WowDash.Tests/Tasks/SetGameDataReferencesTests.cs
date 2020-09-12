using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WowDash.ApplicationCore.DTO;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class SetGameDataReferencesTests : UnitTestBase
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
        public void GivenAValidTask_AndValidGameDataReferences_AddsDataReferencesToDatabase()
        {
            // Arrange
            var task = new Task(_defaultPlayer.Id, TaskType.Collectible);

            Context.Add(task);
            Context.SaveChanges();

            var gameDataReferences = new List<GameDataReferenceItem>();

            var firstExpectedReference = new GameDataReferenceItem(
                null,
                13502,
                GameDataReference.GameDataType.Achievement,
                null,
                "Use the Secret Fish Goggles to collect all the secret fish.");

            var secondExpectedReference = new GameDataReferenceItem(
                null,
                168016,
                GameDataReference.GameDataType.Item,
                "Toy",
                "Hyper-Compressed Ocean");

            gameDataReferences.Add(firstExpectedReference);
            gameDataReferences.Add(secondExpectedReference);

            var dto = new SetGameDataReferencesRequest(task.Id, gameDataReferences);

            // Act
            var result = _controller.SetGameDataReferences(dto);

            var foundTask = Context.Tasks.Find(result.Value);

            // Assert
            foundTask.GameDataReferences.Should().NotBeEmpty();
            foundTask.GameDataReferences.Count.Should().Be(2);
            foundTask.GameDataReferences.All(gdr => gdr.Id != 0);
            foundTask.GameDataReferences.Any(gdr => gdr.GameId == firstExpectedReference.GameId).Should().BeTrue();
            foundTask.GameDataReferences.Any(gdr => gdr.GameId == secondExpectedReference.GameId).Should().BeTrue();
        }

        [Test]
        public void GivenAValidTask_AndMatchingReferencesExist_OnlyAddsNewReferences()
        {
            // Arrange
            var task = new Task(_defaultPlayer.Id, TaskType.Collectible);

            var existingReference = new GameDataReference(
                13502,
                GameDataReference.GameDataType.Achievement,
                null,
                "Use the Secret Fish Goggles to collect all the secret fish.");

            task.GameDataReferences.Add(existingReference);

            Context.Add(task);
            Context.SaveChanges();

            var gameDataReferences = new List<GameDataReferenceItem>();

            var firstExpectedReference = new GameDataReferenceItem(
                null,
                13502,
                GameDataReference.GameDataType.Achievement,
                null,
                "Use the Secret Fish Goggles to collect all the secret fish.");

            var secondExpectedReference = new GameDataReferenceItem(
                null,
                168016,
                GameDataReference.GameDataType.Item,
                "Toy",
                "Hyper-Compressed Ocean");

            gameDataReferences.Add(firstExpectedReference);
            gameDataReferences.Add(secondExpectedReference);

            var dto = new SetGameDataReferencesRequest(task.Id, gameDataReferences);

            // Act
            var result = _controller.SetGameDataReferences(dto);

            var foundTask = Context.Tasks.Find(result.Value);

            // Assert
            foundTask.GameDataReferences.Count.Should().Be(2);
            foundTask.GameDataReferences.Any(gdr => gdr.GameId == firstExpectedReference.GameId).Should().BeTrue();
            foundTask.GameDataReferences.Any(gdr => gdr.GameId == secondExpectedReference.GameId).Should().BeTrue();
        }

        [Test]
        public void GivenAValidTask_AndReferencesExist_ThatAreNotInGivenList_RemovesExtraReferences()
        {
            // Arrange
            var task = new Task(_defaultPlayer.Id, TaskType.Collectible);

            var existingReference = new GameDataReference(
                19019,
                GameDataReference.GameDataType.Item,
                null,
                "Thunderfury, Blessed Blade of the Windseeker");

            task.GameDataReferences.Add(existingReference);

            Context.Add(task);
            Context.SaveChanges();

            var gameDataReferences = new List<GameDataReferenceItem>();

            var firstExpectedReference = new GameDataReferenceItem(
                null,
                13502,
                GameDataReference.GameDataType.Achievement,
                null,
                "Use the Secret Fish Goggles to collect all the secret fish.");

            var secondExpectedReference = new GameDataReferenceItem(
                null,
                168016,
                GameDataReference.GameDataType.Item,
                "Toy",
                "Hyper-Compressed Ocean");

            gameDataReferences.Add(firstExpectedReference);
            gameDataReferences.Add(secondExpectedReference);

            var dto = new SetGameDataReferencesRequest(task.Id, gameDataReferences);

            // Act
            var result = _controller.SetGameDataReferences(dto);

            var foundTask = Context.Tasks.Find(result.Value);

            // Assert
            foundTask.GameDataReferences.Count.Should().Be(2);
            foundTask.GameDataReferences.Any(gdr => gdr.GameId == existingReference.GameId).Should().BeFalse();
            foundTask.GameDataReferences.Any(gdr => gdr.GameId == firstExpectedReference.GameId).Should().BeTrue();
            foundTask.GameDataReferences.Any(gdr => gdr.GameId == secondExpectedReference.GameId).Should().BeTrue();
        }

        [Test]
        public void GivenAValidTask_AndNoReferences_RemovesAllReferences()
        {
            // Arrange
            var task = new Task(_defaultPlayer.Id, TaskType.Collectible);

            var existingReference = new GameDataReference(
                19019,
                GameDataReference.GameDataType.Item,
                null,
                "Thunderfury, Blessed Blade of the Windseeker");

            task.GameDataReferences.Add(existingReference);

            Context.Add(task);
            Context.SaveChanges();

            var dto = new SetGameDataReferencesRequest(task.Id, null);

            // Act
            var result = _controller.SetGameDataReferences(dto);

            var foundTask = Context.Tasks.Find(result.Value);

            // Assert
            foundTask.GameDataReferences.Count.Should().Be(0);
            foundTask.GameDataReferences.Any(gdr => gdr.GameId == existingReference.GameId).Should().BeFalse();
        }

        [Test]
        public void GivenAnInvalidTask_ReturnsNotFound()
        {
            // Arrange
            var dto = new SetGameDataReferencesRequest(TestConstants.AllOnesGuid, null);

            // Act
            var result = _controller.SetGameDataReferences(dto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
