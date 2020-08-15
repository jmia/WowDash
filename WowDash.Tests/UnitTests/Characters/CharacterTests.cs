using NUnit.Framework;
using System.Linq;
using WowDash.ApplicationCore.Entities;
using WowDash.Tests.Common;

namespace WowDash.Tests.UnitTests.Characters
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
            Assume.That(Context.Players.Any(), "The testing database needs at least one user.");

            var defaultUser = Context.Players.First();
            var newCharacter = new Character
            {
                Name = "Meraddison",
                Gender = CharacterGender.Female,
                Realm = "area-52",
                Class = "Warlock",
                Race = "Undead",
                Level = 120,
                PlayerId = defaultUser.Id
            };

            // Act
            var result = Context.Characters.Add(newCharacter);
            var foundCharacter = Context.Characters.Find(result.Entity.Id);

            // Assert
            Assert.That(foundCharacter.Class.Equals("Warlock"));
        }
    }
}