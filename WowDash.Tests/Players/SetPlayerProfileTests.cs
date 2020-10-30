using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Players
{
    [TestFixture]
    public class SetPlayerProfileTests : UnitTestBase
    {
        private PlayersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new PlayersController(Context);
        }

        [TestCase(null, null, null)]
        [TestCase("Sylvanas Windrunner",null, null)]
        [TestCase("Sylvanas Windrunner",TaskType.Collectible, null)]
        [TestCase("Sylvanas Windrunner",TaskType.Collectible, "anvilmar")]
        public void GivenValidProfileValues_UpdatesPlayerInDatabase(string name, TaskType? taskType, string realm)
        {
            // Arrange
            var player = new Player();
            Context.Players.Add(player);
            Context.SaveChanges();

            var dto = new SetPlayerProfileRequest(player.Id, name, taskType, realm);

            // Act
            var result = _controller.SetPlayerProfile(dto);

            // Assert
            var foundPlayer = Context.Players.Find(result.Value);
            foundPlayer.DisplayName.Should().Be(name);
            foundPlayer.DefaultTaskType.Should().Be(taskType);
            foundPlayer.DefaultRealm.Should().Be(realm);
        }

        [Test]
        public void GivenAnInvalidPlayerId_ReturnsNotFound()
        {
            // Arrange
            var dto = new SetPlayerProfileRequest(TestConstants.AllOnesGuid, null, null, null);

            // Act
            var result = _controller.SetPlayerProfile(dto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
