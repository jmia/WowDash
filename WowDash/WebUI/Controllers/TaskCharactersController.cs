using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        /// <param name="characterId">The ID of the character.</param>
        /// <param name="taskId">The ID of the task.</param>
        /// <returns></returns>
        [HttpPut("add/task/{taskId}/character/{characterId}")]
        [HttpPut("add/character/{characterId}/task/{taskId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult AddCharacterToTask(Guid characterId, Guid taskId)
        {
            var task = _context.Tasks.Find(taskId);

            if (task is null)
                return NotFound();

            var character = _context.Characters.Find(characterId);

            if (character is null)
                return NotFound();

            var taskCharacter = new TaskCharacter(characterId, taskId);

            _context.TaskCharacters.Add(taskCharacter);
            _context.SaveChanges();

            return NoContent();

            // TODO: Update the XML docs.
        }

        /// <summary>
        /// Removes a character from a task.
        /// </summary>
        /// <param name="characterId">The ID of the character.</param>
        /// <param name="taskId">The ID of the task.</param>
        /// <returns></returns>
        [HttpDelete("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<TaskCharacter> RemoveCharacterFromTask(Guid characterId, Guid taskId)
        {
            var taskCharacter = _context.TaskCharacters.Find(characterId, taskId);

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
                    return NoContent();

                foreach (var taskCharacter in taskCharacters)
                {
                    taskCharacter.IsActive = true;
                }
            }

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
