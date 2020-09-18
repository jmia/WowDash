using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;

namespace WowDash.UnitTests.TaskCharacters
{
    [TestFixture]
    public class GetTaskCharactersByCharacterIdTests : UnitTestBase
    {
        private TaskCharactersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TaskCharactersController(Context);
        }

        [Test]
        public void GivenAValidCharacterId_ReturnsTaskCharacters()
        {

        }

        [Test]
        public void GivenAValidCharacterId_AndNoTaskCharacters_ReturnsEmptyList()
        {

        }

        [Test]
        public void GivenAnInvalidCharacterId_ReturnsNotFound()
        {

        }
    }
}
