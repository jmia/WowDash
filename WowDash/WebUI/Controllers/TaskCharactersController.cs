using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WowDash.ApplicationCore.DTO;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/task-characters")]
    [ApiController]
    public class TaskCharactersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskCharactersController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a character to a task.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TaskCharacter> AddCharacterToTask(AddCharacterToTaskRequest request)
        {
            // TODO: Validation

            var taskCharacter = new TaskCharacter(request.CharacterId, request.TaskId);

            _context.TaskCharacters.Add(taskCharacter);
            _context.SaveChanges();

            // TODO: Change the return type
            return taskCharacter;

            // TODO: Update the XML docs.
        }

        /// <summary>
        /// Removes a character from a task.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Sets a character's attempt complete for the task's refresh frequency.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("complete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Sets a character's attempt incomplete (re-enables attempt) for the task's refresh frequency.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch("incomplete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Resets all attempts for characters on tasks with a daily refresh frequency.
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh/daily")]
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
                    continue;

                foreach (var taskCharacter in taskCharacters)
                {
                    taskCharacter.IsActive = true;
                }
            }

            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Resets all attempts for characters on tasks with a daily refresh frequency.
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh/weekly")]
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
                    continue;

                foreach (var taskCharacter in taskCharacters)
                {
                    taskCharacter.IsActive = true;
                }
            }

            _context.SaveChanges();

            return NoContent();
        }
    }
}
