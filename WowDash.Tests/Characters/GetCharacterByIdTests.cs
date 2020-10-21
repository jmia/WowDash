using FluentAssertions;
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
    public class GetCharacterByIdTests : UnitTestBase
    {
        private CharactersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new CharactersController(Context);
        }

        [Test]
        public void GivenAValidCharacterId_ReturnsCharacter()
        {
            // Arrange
            var expectedName = "Mozart";
            var expectedGender = CharacterGender.Female;
            var expectedClass = "Monk";
            var expectedSpecialization = "Brewmaster";
            var expectedRace = "Pandaren";
            var expectedRealm = "area-52";
            var character = new Character(DefaultPlayer.Id, null, expectedName, expectedGender, null, expectedClass,
                expectedSpecialization, expectedRace, expectedRealm);

            Context.Characters.Add(character);
            Context.SaveChanges();

            // Act
            var response = _controller.GetCharacterById(character.Id);

            // Assert
            response.Value.Should().NotBeNull();
            Assert.IsInstanceOf<CharacterResponse>(response.Value);
            response.Value.GameId.Should().BeNull();
            response.Value.Name.Should().Be(expectedName);
            response.Value.Gender.Should().Be(expectedGender);
            response.Value.Class.Should().Be(expectedClass);
            response.Value.Specialization.Should().Be(expectedSpecialization);
            response.Value.Race.Should().Be(expectedRace);
            response.Value.Realm.Should().Be(expectedRealm);
        }

        [Test]
        public void GivenAnInvalidCharacterId_ReturnsNotFound()
        {
            // Arrange, Act
            var response = _controller.GetCharacterById(TestConstants.AllOnesGuid);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(response.Result);
        }
    }
}
