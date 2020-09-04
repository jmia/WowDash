﻿using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.Json;
using WowDash.ApplicationCore.Entities;

namespace WowDash.IntegrationTests
{
    [TestFixture]
    public class UnitTest2 : IntegrationTestBase
    {
        [SetUp]
        public void RunBeforeEachTest()
        {
            // Base setup is called first which wipes the database
            // Then need to access the context and seed some data for this test suite
        }

        [Test]
        public async System.Threading.Tasks.Task GetCharacters_ShouldFindSomething()
        {
            // Act
            var httpResponse = await Client.GetAsync("/api/Characters");

            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadAsStreamAsync();
            var characters = await JsonSerializer.DeserializeAsync<IEnumerable<Character>>(response);
            
            // Assert
            characters.Should().HaveCount(2);   // Count is 0 when using checkpoints, count goes up by 2 each run without them
        }
    }
}