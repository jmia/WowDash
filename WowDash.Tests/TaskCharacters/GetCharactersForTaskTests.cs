using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using WowDash.ApplicationCore.DTO.Responses;
using WowDash.ApplicationCore.Entities;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.UnitTests.TaskCharacters
{
    [TestFixture]
    public class GetCharactersForTaskTests : UnitTestBase
    {
        private TaskCharactersController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TaskCharactersController(Context);
        }
        
        [Test]
        public void GivenAValidTaskId_ReturnsAssociatedCharacters()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.Collectible);
            var firstCharacter = new Character() { PlayerId = DefaultPlayer.Id, Name = "Beatrix" };
            var secondCharacter = new Character() { PlayerId = DefaultPlayer.Id, Name = "Archer" };

            Context.Tasks.Add(task);
            Context.Characters.AddRange(firstCharacter, secondCharacter);
            Context.SaveChanges();

            var firstTaskCharacter = new TaskCharacter(firstCharacter.Id, task.Id);
            var secondTaskCharacter = new TaskCharacter(secondCharacter.Id, task.Id);

            Context.TaskCharacters.AddRange(firstTaskCharacter, secondTaskCharacter);
            Context.SaveChanges();

            // Act
            var result = _controller.GetCharactersForTask(task.Id);

            // Assert
            Assert.IsInstanceOf<GetCharactersForTaskResponse>(result.Value);
            result.Value.Characters.Count.Should().Be(2);
            result.Value.Characters.Any(c => c.CharacterId == firstCharacter.Id).Should().BeTrue();
            result.Value.Characters.Any(c => c.CharacterId == secondCharacter.Id).Should().BeTrue();
        }

        [Test]
        public void GivenAnInvalidTaskId_ReturnsEmptyList()
        {
            // Arrange, Act
            var result = _controller.GetCharactersForTask(TestConstants.AllOnesGuid);

            // Assert
            result.Value.TaskId.Should().Be(TestConstants.AllOnesGuid);
            result.Value.Characters.Should().BeEmpty();
        }

        [Test]
        public void GivenAValidTaskId_AndNoAssociatedCharacters_ReturnsEmptyList()
        {
            // Arrange
            var task = new Task(DefaultPlayer.Id, TaskType.Collectible);
            
            Context.Tasks.Add(task);
            Context.SaveChanges();

            // Act
            var result = _controller.GetCharactersForTask(task.Id);

            // Assert
            result.Value.TaskId.Should().Be(task.Id);
            result.Value.Characters.Should().BeEmpty();
        }
    }
}
