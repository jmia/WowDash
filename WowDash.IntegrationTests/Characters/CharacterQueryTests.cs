using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowDash.ApplicationCore.Entities;

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
    }
}
