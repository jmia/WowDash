using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WowDash.ApplicationCore.DTO.Common;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.UnitTests.Common;
using WowDash.WebUI.Controllers;
using static WowDash.ApplicationCore.Common.Enums;
using static WowDash.ApplicationCore.Entities.GameDataReference;

namespace WowDash.UnitTests.Tasks
{
    [TestFixture]
    public class AddTaskTests : UnitTestBase
    {
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new TasksController(Context);
        }

        [Test]
        public void GivenAnInvalidUser_DoesNotAddTaskToDatabase()
        {
            // Arrange
            var dto = new AddTaskRequest()
            {
                PlayerId = TestConstants.AllOnesGuid
            };

            // Act
            var result = _controller.AddTask(dto);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);

        }

        //[Required]
        //public Guid PlayerId { get; set; }
        //public string Description { get; set; }
        //public ICollection<GameDataReferenceItem> GameDataReferenceItems { get; set; }
        //[Required]
        //public bool IsFavourite { get; set; }
        //public string Notes { get; set; }
        //[Required]
        //public TaskType TaskType { get; set; }
        //public CollectibleType? CollectibleType { get; set; }
        //public Source? Source { get; set; }
        //[Required]
        //public Priority Priority { get; set; }
        //[Required]
        //public RefreshFrequency RefreshFrequency { get; set; }

        //public AddTaskRequest()
        //{
        //    GameDataReferenceItems = new List<GameDataReferenceItem>();
        //}

        // Second biggest cop-out test of all time for the 
        // second biggest cop-out method of all time
        [Test]
        public void GivenValidUser_AndAllValidProperties_AddsTaskToDatabase()
        {
            // Arrange
            var dto = new AddTaskRequest()
            {
                PlayerId = DefaultPlayer.Id,
                TaskType = TaskType.Achievement,
                Description = "Secret Fish and Where To Find Them",
                IsFavourite = true,
                Notes = null,
                CollectibleType = null,
                Source = null,
                Priority = Priority.Medium,
                RefreshFrequency = RefreshFrequency.Never
            };

            dto.GameDataReferenceItems.Add(
                new GameDataReferenceItem() 
                { 
                    Id = 2, 
                    GameId = 13502, 
                    Type = GameDataType.Achievement, 
                    Subclass = null, 
                    Description = "Secret Fish and Where To Find Them" 
                });

            // Act
            var result = _controller.AddTask(dto);

            // Assert
            var foundTask = Context.Tasks.Find(result.Value);

            foundTask.PlayerId.Should().Be(dto.PlayerId);
            foundTask.TaskType.Should().Be(dto.TaskType);
            foundTask.Description.Should().Be(dto.Description);
            foundTask.IsFavourite.Should().Be(dto.IsFavourite);
            foundTask.Notes.Should().Be(dto.Notes);
            foundTask.CollectibleType.Should().Be(dto.CollectibleType);
            foundTask.Source.Should().Be(dto.Source);
            foundTask.Priority.Should().Be(dto.Priority);
            foundTask.RefreshFrequency.Should().Be(dto.RefreshFrequency);
        }
    }
}
