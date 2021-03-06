﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WowDash.ApplicationCore.DTO.Responses;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Characters
{
    [TestFixture]
    public class GetCharacterRosterTests : UnitTestBase
    {
        private CharactersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CharactersController(Context);
        }

        [Test]
        public void GivenAValidPlayerId_ReturnsCharacters()
        {
            // Arrange
            var character = new Character(DefaultPlayer.Id, null, "Mozart", CharacterGender.Female, null, "Monk",
                "Brewmaster", "Pandaren", "area-52");
            var secondCharacter = new Character(DefaultPlayer.Id, null, "Oleander", CharacterGender.Female, null, "Mage",
                "Frost", "Undead", "area-52");

            Context.Characters.AddRange(character, secondCharacter);
            Context.SaveChanges();

            // Act
            var response = _controller.GetCharacterRoster(DefaultPlayer.Id);

            // Assert
            response.Value.Should().NotBeNull();
            Assert.IsInstanceOf<GetCharacterRosterResponse>(response.Value);
            response.Value.Characters.Count.Should().Be(2);
            response.Value.PlayerId.Should().Be(DefaultPlayer.Id);
        }

        [Test]
        public void GivenAnInvalidPlayerId_ReturnsNotFound()
        {
            // Arrange, Act
            var response = _controller.GetCharacterRoster(TestConstants.AllOnesGuid);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }
    }
}
