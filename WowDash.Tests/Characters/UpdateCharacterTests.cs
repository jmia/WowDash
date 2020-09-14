using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Linq;
using WowDash.ApplicationCore.DTO;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;

namespace WowDash.UnitTests.Characters
{
    [TestFixture]
    public class UpdateCharacterTests : UnitTestBase
    {
        private Player _defaultPlayer;
        private CharactersController _controller;

        [SetUp]
        public void Setup()
        {
            Assume.That(Context.Players.Any(), "The testing database needs at least one user.");
            _defaultPlayer = Context.Players.First();
            _controller = new CharactersController(Context);
        }

        [Test]
        public void GivenValidCharacter_UpdatesCharacter()
        {
            // Arrange
            var character = new Character
            {
                PlayerId = _defaultPlayer.Id
            };

            Context.Characters.Add(character);
            Context.SaveChanges();

            var expectedName = "Meraddison";
            var expectedClass = "Warlock";
            var expectedLevel = 120;
            var expectedRealm = "area-52";
            var expectedRace = "Undead";
            var expectedGender = CharacterGender.Female;
            int? expectedGameId = null;

            var dto = new UpdateCharacterRequest(character.Id, expectedGameId, expectedName, expectedGender,
                expectedLevel, expectedClass, expectedRace, expectedRealm);

            // Act
            var result = _controller.UpdateCharacter(dto);

            var foundCharacter = Context.Characters.Find(result.Value);

            // Assert
            foundCharacter.Should().NotBeNull();
            foundCharacter.PlayerId.Should().Be(_defaultPlayer.Id);
            foundCharacter.Name.Should().Be(expectedName);
            foundCharacter.GameId.Should().BeNull();
            foundCharacter.Class.Should().Be(expectedClass);
            foundCharacter.Race.Should().Be(expectedRace);
            foundCharacter.Level.Should().Be(expectedLevel);
            foundCharacter.Gender.Should().Be(expectedGender);
            foundCharacter.Realm.Should().Be(expectedRealm);
        }

        [Test]
        public void GivenSomeNullProperties_OverwritesExistingPropertiesInDatabase()
        {
            // Arrange
            var character = new Character
            {
                PlayerId = _defaultPlayer.Id,
                Name = "Meraddison",
                Class = "Warlock",
                Level = 120,
                Realm = "area-52",
                Gender = CharacterGender.Female
            };

            Context.Characters.Add(character);
            Context.SaveChanges();

            string expectedName = null;
            string expectedClass = null;
            int? expectedLevel = null;
            var expectedRealm = "area-52";
            var expectedRace = "Undead";
            var expectedGender = CharacterGender.Female;
            int? expectedGameId = null;

            var dto = new UpdateCharacterRequest(character.Id, expectedGameId, expectedName, expectedGender,
                expectedLevel, expectedClass, expectedRace, expectedRealm);

            // Act
            var result = _controller.UpdateCharacter(dto);

            var foundCharacter = Context.Characters.Find(result.Value);

            // Assert
            foundCharacter.Should().NotBeNull();
            foundCharacter.PlayerId.Should().Be(_defaultPlayer.Id);
            foundCharacter.Name.Should().Be(expectedName);
            foundCharacter.GameId.Should().BeNull();
            foundCharacter.Class.Should().Be(expectedClass);
            foundCharacter.Race.Should().Be(expectedRace);
            foundCharacter.Level.Should().Be(expectedLevel);
            foundCharacter.Gender.Should().Be(expectedGender);
            foundCharacter.Realm.Should().Be(expectedRealm);
        }

        [Test]
        public void GivenAnInvalidCharacterId_ReturnsNotFound()
        {
            // Arrange
            var dto = new UpdateCharacterRequest(TestConstants.AllOnesGuid, null, null, default, null, null, null, null);

            // Act
            var result = _controller.UpdateCharacter(dto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}
