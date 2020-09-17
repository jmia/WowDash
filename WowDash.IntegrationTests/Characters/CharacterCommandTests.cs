using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using WowDash.ApplicationCore.Entities;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.IntegrationTests.Characters
{
    [TestFixture]
    public class CharacterCommandTests : IntegrationTestBase
    {
        internal Guid defaultPlayerId;

        [SetUp]
        public async System.Threading.Tasks.Task Setup()
        {
            var defaultPlayer = await AddAsync(new Player() { DisplayName = "Jen" });
            defaultPlayerId = defaultPlayer.Id;
        }

        [Test]
        public async System.Threading.Tasks.Task CreateCharacter_CreatesCharacter()
        {
            //{
            //  "playerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //  "gameId": 0,
            //  "name": "string",
            //  "gender": 0,
            //  "level": 0,
            //  "class": "string",
            //  "race": "string",
            //  "realm": "string"
            //}

            var expectedGameId = 0;
            var expectedName = "Orbrand";
            var expectedGender = CharacterGender.Female;
            var expectedLevel = 64;
            var expectedClass = "Death Knight";
            var expectedRace = "Zandalari Troll";
            var expectedRealm = "area-52";

            string json;

            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream, options))
                {
                    writer.WriteStartObject();
                    writer.WriteString("playerId", defaultPlayerId);
                    writer.WriteNumber("gameId", expectedGameId);
                    writer.WriteString("name", expectedName);
                    writer.WriteNumber("gender", (int)expectedGender);
                    writer.WriteNumber("level", expectedLevel);
                    writer.WriteString("class", expectedClass);
                    writer.WriteString("race", expectedRace);
                    writer.WriteString("realm", expectedRealm);
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PostAsync("/api/characters", content);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundCharacter = await FindAsync<Character>(result);

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
        public async System.Threading.Tasks.Task UpdateCharacter_UpdatesCharacter()
        {
            //{
            //  "characterId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            //  "gameId": 0,
            //  "name": "string",
            //  "gender": 0,
            //  "level": 0,
            //  "class": "string",
            //  "race": "string",
            //  "realm": "string"
            //}

            var character = await AddAsync(new Character() { PlayerId = defaultPlayerId });

            var expectedGameId = 0;
            var expectedName = "Orbrand";
            var expectedGender = CharacterGender.Female;
            var expectedLevel = 64;
            var expectedClass = "Death Knight";
            var expectedRace = "Zandalari Troll";
            var expectedRealm = "area-52";

            string json;

            var options = new JsonWriterOptions
            {
                Indented = true
            };

            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream, options))
                {
                    writer.WriteStartObject();
                    writer.WriteString("characterId", character.Id);
                    writer.WriteNumber("gameId", expectedGameId);
                    writer.WriteString("name", expectedName);
                    writer.WriteNumber("gender", (int)expectedGender);
                    writer.WriteNumber("level", expectedLevel);
                    writer.WriteString("class", expectedClass);
                    writer.WriteString("race", expectedRace);
                    writer.WriteString("realm", expectedRealm);
                    writer.WriteEndObject();
                }

                json = Encoding.UTF8.GetString(stream.ToArray());
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var httpResponse = await Client.PutAsync("/api/characters", content);

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundCharacter = await FindAsync<Character>(result);

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
        public async System.Threading.Tasks.Task DeleteCharacter_DeletesCharacter()
        {
            //{
            //    "characterId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            //}

            // Arrange
            var character = await AddAsync(new Character() { PlayerId = defaultPlayerId });

            // Act
            var httpResponse = await Client.DeleteAsync($"/api/characters/{character.Id}");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<Guid>(response);

            var foundCharacter = FindAsync<Character>(result).Result;

            // Assert
            foundCharacter.Should().BeNull();
        }
    }
}
