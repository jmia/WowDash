﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.Characters
{
    [TestFixture]
    public class AddCharacterTests : UnitTestBase
    {
        private CharactersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CharactersController(Context);
        }

        [Test]
        public void GivenACompleteValidCharacter_AddsCharacter()
        {
            // Arrange
            var expectedName = "Meraddison";
            var expectedClass = "Warlock";
            var expectedSpecialization = "Affliction";
            var expectedLevel = 120;
            var expectedRealm = "area-52";
            var expectedRace = "Undead";
            var expectedGender = CharacterGender.Female;
            int? expectedGameId = null;

            var dto = new AddCharacterRequest(DefaultPlayer.Id, expectedGameId, expectedName, expectedGender,
                expectedLevel, expectedClass, expectedSpecialization, expectedRace, expectedRealm);

            // Act
            var result = _controller.AddCharacter(dto);

            var foundCharacter = Context.Characters.Find(result.Value);

            // Assert
            foundCharacter.Should().NotBeNull();
            foundCharacter.PlayerId.Should().Be(DefaultPlayer.Id);
            foundCharacter.Name.Should().Be(expectedName);
            foundCharacter.GameId.Should().BeNull();
            foundCharacter.Class.Should().Be(expectedClass);
            foundCharacter.Race.Should().Be(expectedRace);
            foundCharacter.Level.Should().Be(expectedLevel);
            foundCharacter.Gender.Should().Be(expectedGender);
            foundCharacter.Realm.Should().Be(expectedRealm);
        }

        [TestCase(null, null, default, null, null, null, null, null)]
        [TestCase(123456789, null, default, null, null, null, null, null)]
        [TestCase(123456789, "Rhuue", default, null, null, null, null, null)]
        [TestCase(123456789, "Zenebatos", CharacterGender.Male, null, null, null, null, null)]
        [TestCase(123456789, "Rhuue", CharacterGender.Female, 100, null, null, null, null)]
        [TestCase(123456789, "Rhuue", CharacterGender.Female, 100, "Hunter", null, null, null)]
        [TestCase(123456789, "Rhuue", CharacterGender.Female, 100, "Hunter", "Marksmanship", "Vulpera", null)]
        public void GivenSomeNullOrDefaultProperties_AddsCharacter(int? gameId, string name,
            CharacterGender gender, int? level, string @class, string specialization, string race, string realm)
        {
            // Arrange
            var dto = new AddCharacterRequest(DefaultPlayer.Id, gameId, name, gender,
                level, @class, specialization, race, realm);

            // Act
            var result = _controller.AddCharacter(dto);

            var foundCharacter = Context.Characters.Find(result.Value);

            // Assert
            foundCharacter.Should().NotBeNull();
            foundCharacter.PlayerId.Should().Be(DefaultPlayer.Id);
            foundCharacter.Name.Should().Be(name);
            foundCharacter.GameId.Should().Be(gameId);
            foundCharacter.Class.Should().Be(@class);
            foundCharacter.Specialization.Should().Be(specialization);
            foundCharacter.Race.Should().Be(race);
            foundCharacter.Level.Should().Be(level);
            foundCharacter.Gender.Should().Be(gender);
            foundCharacter.Realm.Should().Be(realm);
        }

        [Test]
        public void GivenAnInvalidPlayerId_ReturnsNotFound()
        {
            // Arrange
            var dto = new AddCharacterRequest(TestConstants.AllOnesGuid, null, null, default, null, null, null, null, null);

            // Act
            var result = _controller.AddCharacter(dto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
