using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text.Json;
using WowDash.ApplicationCore.DTO.Responses;
using WowDash.ApplicationCore.Entities;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.IntegrationTests.Characters
{
    [TestFixture]
    public class CharacterQueryTests : IntegrationTestBase
    {
        internal Guid defaultPlayerId;

        [SetUp]
        public async System.Threading.Tasks.Task Setup()
        {
            var defaultPlayer = await AddAsync(new Player() { DisplayName = "Jen" });
            defaultPlayerId = defaultPlayer.Id;
        }

        [Test]
        public async System.Threading.Tasks.Task GetCharacterById_ReturnsCharacter()
        {
            var expectedGameId = 0;
            var expectedName = "Starling";
            var expectedGender = CharacterGender.Female;
            var expectedLevel = 60;
            var expectedClass = "Rogue";
            var expectedRace = "Undead";
            var expectedRealm = "dark-iron";

            var character = await AddAsync(
                new Character(
                    defaultPlayerId, expectedGameId, expectedName, expectedGender, expectedLevel, expectedClass,
                    expectedRace, expectedRealm));

            // Act
            var httpResponse = await Client.GetAsync($"/api/characters/{character.Id}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<CharacterResponse>(response, options);

            var foundCharacter = await FindAsync<Character>(result.CharacterId);

            // Assert
            foundCharacter.Should().NotBeNull();
            foundCharacter.PlayerId.Should().Be(defaultPlayerId);
            foundCharacter.Name.Should().Be(expectedName);
            foundCharacter.GameId.Should().Be(expectedGameId);
            foundCharacter.Class.Should().Be(expectedClass);
            foundCharacter.Race.Should().Be(expectedRace);
            foundCharacter.Level.Should().Be(expectedLevel);
            foundCharacter.Gender.Should().Be(expectedGender);
            foundCharacter.Realm.Should().Be(expectedRealm);
        }

        [Test]
        public async System.Threading.Tasks.Task GetPlayerCharacters_ReturnsCharacters()
        {
            var expectedGameId = 0;
            var expectedName = "Starling";
            var expectedGender = CharacterGender.Female;
            var expectedLevel = 60;
            var expectedClass = "Rogue";
            var expectedRace = "Undead";
            var expectedRealm = "dark-iron";

            await AddAsync(new Character(
                    defaultPlayerId, expectedGameId, expectedName, expectedGender, expectedLevel, expectedClass,
                    expectedRace, expectedRealm));

            // Act
            var httpResponse = await Client.GetAsync($"/api/characters/all/{defaultPlayerId}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = await JsonSerializer.DeserializeAsync<GetCharactersResponse>(response, options);

            // Assert
            result.Should().NotBeNull();
            result.PlayerId.Should().Be(defaultPlayerId);
            result.Characters.Count.Should().Be(1);

            var foundCharacter = await FindAsync<Character>(result.Characters.FirstOrDefault().CharacterId);

            foundCharacter.Should().NotBeNull();
            foundCharacter.PlayerId.Should().Be(defaultPlayerId);
            foundCharacter.Name.Should().Be(expectedName);
            foundCharacter.GameId.Should().Be(expectedGameId);
            foundCharacter.Class.Should().Be(expectedClass);
            foundCharacter.Race.Should().Be(expectedRace);
            foundCharacter.Level.Should().Be(expectedLevel);
            foundCharacter.Gender.Should().Be(expectedGender);
            foundCharacter.Realm.Should().Be(expectedRealm);
        }
    }
}
