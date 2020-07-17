using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using wow_dashboard.Models;
using wow_dashboard_tests.Common;

namespace wow_dashboard_tests.UnitTests.Characters
{
    public class CharacterTests : UnitTestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddCharacter_ValidCharacter_AddsToDatabase()
        {
            // Arrange
            Assume.That(Context.Users.Any(), "The testing database needs at least one user.");

            var defaultUser = Context.Users.First();
            var newCharacter = new Character()
            {
                Name = "Meraddison",
                Class = PlayableClass.Warlock,
                PlayableRaceGameId = 5,
                Level = 120,
                Professions = new List<Profession>
                {
                    Profession.Tailoring,
                    Profession.Enchanting
                },
                UserId = defaultUser.Id
            };

            // Act
            var result = Context.Characters.Add(newCharacter);
            var foundCharacter = Context.Characters.Find(result.Entity.Id);

            // Assert
            Assert.That(foundCharacter.Professions.Contains(Profession.Tailoring));
        }
    }
}