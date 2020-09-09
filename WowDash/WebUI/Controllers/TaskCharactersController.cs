using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WowDash.ApplicationCore.DTO;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.WebUI.Controllers
{
    [Route("api/task-characters")]
    [ApiController]
    public class TaskCharactersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskCharactersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<TaskCharacter> AddCharacterToTask(AddCharacterToTaskRequest request)
        {
            var taskCharacter = new TaskCharacter(request.CharacterId, request.TaskId);

            _context.TaskCharacters.Add(taskCharacter);
            _context.SaveChanges();

            return taskCharacter;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<TaskCharacter> RemoveCharacterFromTask(RemoveCharacterFromTaskRequest request)
        {
            var taskCharacter = _context.TaskCharacters.Find(request.CharacterId, request.TaskId);

            if (taskCharacter is null)
                return NotFound();

            _context.TaskCharacters.Remove(taskCharacter);
            _context.SaveChanges();

            return taskCharacter;
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TaskCharacter> SetAttemptComplete(SetAttemptCompleteRequest request)
        {
            var taskCharacter = _context.TaskCharacters.Find(request.CharacterId, request.TaskId);

            if (taskCharacter is null)
                return NotFound();

            taskCharacter.IsActive = false;
            _context.SaveChanges();

            return taskCharacter;
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TaskCharacter> SetAttemptIncomplete(SetAttemptIncompleteRequest request)
        {
            var taskCharacter = _context.TaskCharacters.Find(request.CharacterId, request.TaskId);

            if (taskCharacter is null)
                return NotFound();

            taskCharacter.IsActive = true;
            _context.SaveChanges();

            return taskCharacter;
        }

        // Is this a Tasks controller method or a TaskCharacters controller method?
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult RefreshDailyTaskCharacters()
        {
            var tasks = _context.Tasks.Where(t => t.RefreshFrequency == RefreshFrequency.Daily);

            if (tasks is null)
                return NoContent();

            foreach (var task in tasks)
            {
                var taskCharacters = _context.TaskCharacters.Where(tc => tc.TaskId == task.Id && tc.IsActive == false);

                if (taskCharacters is null)
                    return NoContent();

                foreach (var taskCharacter in taskCharacters)
                {
                    taskCharacter.IsActive = true;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult RefreshWeeklyTaskCharacters()
        {
            var tasks = _context.Tasks.Where(t => t.RefreshFrequency == RefreshFrequency.Weekly);

            if (tasks is null)
                return NoContent();

            foreach (var task in tasks)
            {
                var taskCharacters = _context.TaskCharacters.Where(tc => tc.TaskId == task.Id && tc.IsActive == false);

                if (taskCharacters is null)
                    return NoContent();

                foreach (var taskCharacter in taskCharacters)
                {
                    taskCharacter.IsActive = true;
                }
            }

            return NoContent();
        }
    }
}
