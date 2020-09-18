using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;

namespace WowDash.UnitTests.TaskCharacters
{
    [TestFixture]
    public class GetTaskCharactersByTaskIdTests : UnitTestBase
    {
        private TaskCharactersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TaskCharactersController(Context);
        }

        [Test]
        public void GivenAValidTaskId_ReturnsTaskCharacters()
        {

        }

        [Test]
        public void GivenAValidTaskId_AndNoTaskCharacters_ReturnsEmptyList()
        {

        }

        [Test]
        public void GivenAnInvalidTaskId_ReturnsNotFound()
        {

        }
    }
}
