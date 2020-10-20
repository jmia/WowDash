using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.ApplicationCore.DTO.Responses;
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
        /// Gets a single task-character for a given composite key.
        /// </summary>
        /// <param name="characterId">The ID of the character.</param>
        /// <param name="taskId">The ID of the task.</param>
        /// <response code="200">Returns the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the resource was not found in the database.</response>
        [HttpGet("{characterId}:{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TaskCharacterResponse> GetTaskCharacterById(Guid characterId, Guid taskId)
        {
            var taskCharacter = _context.TaskCharacters.Find(characterId, taskId);

            if (taskCharacter is null)
                return NotFound();

            var response = new TaskCharacterResponse(taskCharacter.CharacterId, taskCharacter.TaskId,
                taskCharacter.IsActive);

            return response;
        }

        /// <summary>
        /// Gets all characters for a given task.
        /// </summary>
        /// <param name="taskId">The ID of the task.</param>
        /// <returns></returns>
        [HttpGet("task/{taskId}")]
        public ActionResult<GetCharactersForTaskResponse> GetCharactersForTask(Guid taskId)
        {
            var taskCharacters = _context.TaskCharacters.Where(tc => tc.TaskId == taskId);

            var characterList = new List<CharacterForTaskResponse>();

            foreach (var tc in taskCharacters)
            {
                var character = _context.Characters.Find(tc.CharacterId);
                characterList.Add(new CharacterForTaskResponse(character.Id, character.Name, character.Class, tc.IsActive));
            }

            return new GetCharactersForTaskResponse(taskId, characterList);
        }

        /// <summary>
        /// Adds a character to a task.
        /// </summary>
        /// <param name="request">The ID of the character and task.</param>
        /// <response code="204">If the request successfully adds the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the task or character was not found in the database.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult AddCharacterToTask(AddCharacterToTaskRequest request)
        {
            var task = _context.Tasks.Find(request.TaskId);

            if (task is null)
                return NotFound();

            var character = _context.Characters.Find(request.CharacterId);

            if (character is null)
                return NotFound();

            var taskCharacter = new TaskCharacter(request.CharacterId, request.TaskId);

            _context.TaskCharacters.Add(taskCharacter);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Removes a character from a task.
        /// </summary>
        /// <param name="characterId">The ID of the character.</param>
        /// <param name="taskId">The ID of the task.</param>
        /// <response code="204">If the request successfully deletes the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the resource was not found in the database.</response>
        [HttpDelete("{characterId}:{taskId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult RemoveCharacterFromTask(Guid characterId, Guid taskId)
        {
            var taskCharacter = _context.TaskCharacters.Find(characterId, taskId);

            if (taskCharacter is null)
                return NotFound();

            _context.TaskCharacters.Remove(taskCharacter);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Sets a character's attempt complete for the task's refresh frequency.
        /// </summary>
        /// <param name="request">The ID of the character and task.</param>
        /// <response code="204">If the request successfully updates the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the resource was not found in the database.</response>
        [HttpPatch("complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult SetAttemptComplete(SetAttemptCompleteRequest request)
        {
            var taskCharacter = _context.TaskCharacters.Find(request.CharacterId, request.TaskId);

            if (taskCharacter is null)
                return NotFound();

            taskCharacter.IsActive = false;
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Sets a character's attempt incomplete (re-enables attempt) for the task's refresh frequency.
        /// </summary>
        /// <param name="request">The ID of the character and task.</param>
        /// <response code="204">If the request successfully updates the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the resource was not found in the database.</response>
        [HttpPatch("revert")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult SetAttemptIncomplete(SetAttemptIncompleteRequest request)
        {
            var taskCharacter = _context.TaskCharacters.Find(request.CharacterId, request.TaskId);

            if (taskCharacter is null)
                return NotFound();

            taskCharacter.IsActive = true;
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Resets all attempts for characters on tasks with a daily refresh frequency.
        /// </summary>
        /// <response code="204">If the request successfully updates the resources.</response>
        [HttpPost("refresh/daily")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult RefreshDailyTaskCharacters()
        {
            var tasks = _context.Tasks.Where(t => t.RefreshFrequency == RefreshFrequency.Daily);

            foreach (var task in tasks)
            {
                var taskCharacters = _context.TaskCharacters.Where(tc => tc.TaskId == task.Id && tc.IsActive == false);

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
        /// <response code="204">If the request successfully updates the resources.</response>
        [HttpPost("refresh/weekly")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult RefreshWeeklyTaskCharacters()
        {
            var tasks = _context.Tasks.Where(t => t.RefreshFrequency == RefreshFrequency.Weekly);

            foreach (var task in tasks)
            {
                var taskCharacters = _context.TaskCharacters.Where(tc => tc.TaskId == task.Id && tc.IsActive == false);

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
