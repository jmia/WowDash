using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WowDash.ApplicationCore.DTO;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.WebUI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<Guid> InitializeTask(InitializeTaskRequest request)
        {
            var task = new Task(request.PlayerId, request.TaskType);

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return task.Id;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> SetGeneralTaskDetails(SetGeneralTaskDetailsRequest request)
        {
            var task = _context.Tasks.Find(request.TaskId);

            if (task is null)
                return NotFound();

            task.Description = request.Description;
            task.RefreshFrequency = request.RefreshFrequency;
            task.Priority = request.Priority;

            _context.SaveChanges();

            return task.Id;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> SetAchievementTaskDetails(SetAchievementTaskDetailsRequest request)
        {
            var task = _context.Tasks.Find(request.TaskId);

            if (task is null)
                return NotFound();

            task.Description = request.Description;
            task.Priority = request.Priority;

            // Achievements never recur
            task.RefreshFrequency = RefreshFrequency.Never;

            _context.SaveChanges();

            return task.Id;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TaskCharacter> AddCharacterToTask(AddCharacterToTaskRequest request)
        {
            var taskCharacter = new TaskCharacter(request.CharacterId, request.TaskId);

            _context.TaskCharacters.Add(taskCharacter);
            _context.SaveChanges();

            return taskCharacter;
        }
    }
}
