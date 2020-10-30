using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Players
{
    [TestFixture]
    public class GetPlayerProfileTests : UnitTestBase
    {
        private PlayersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new PlayersController(Context);
        }

        [Test]
        public void GivenValidPlayerId_ReturnsPlayerProfile()
        {
            // Arrange
            var player = new Player();
            player.DisplayName = "Jaina Proudmoore";
            player.DefaultRealm = "aegwynn";
            player.DefaultTaskType = TaskType.Achievement;
            Context.Players.Add(player);
            Context.SaveChanges();

            // Act
            var result = _controller.GetPlayerProfile(player.Id);

            // Assert
            var foundPlayer = Context.Players.Find(result.Value.PlayerId);
            foundPlayer.DisplayName.Should().Be(player.DisplayName);
            foundPlayer.DefaultTaskType.Should().Be(player.DefaultTaskType);
            foundPlayer.DefaultRealm.Should().Be(player.DefaultRealm);
        }

        [Test]
        public void GivenAnInvalidPlayerId_ReturnsNotFound()
        {
            // Arrange, Act
            var result = _controller.GetPlayerProfile(TestConstants.AllOnesGuid);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
