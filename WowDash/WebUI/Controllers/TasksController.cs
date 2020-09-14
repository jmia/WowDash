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
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns the ID of the created task.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Guid> InitializeTask(InitializeTaskRequest request)
        {
            var player = _context.Players.Find(request.PlayerId);

            if (player is null)
                return NotFound();

            var task = new Task(player.Id, request.TaskType);

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return task.Id;
        }

        /// <summary>
        /// Adds details to a general task.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns the ID of the updated task.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the task was not found in the database.</response>
        [HttpPatch("general/details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Adds details to an achievement-type task.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns the ID of the updated task.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the task was not found in the database.</response>
        [HttpPatch("achievement/details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Adds details to a collectible task.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns the ID of the updated task.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the task was not found in the database.</response>
        [HttpPatch("collectible/details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> SetCollectibleTaskDetails(SetCollectibleTaskDetailsRequest request)
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

        /// <summary>
        /// Adds collectible type and source to a collectible task.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns the ID of the updated task.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the task was not found in the database.</response>
        [HttpPatch("collectible/type-source")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> SetTaskCollectibleTypeAndSource(SetTaskCollectibleTypeAndSourceRequest request)
        {
            var task = _context.Tasks.Find(request.TaskId);

            if (task is null)
                return NotFound();

            task.CollectibleType = request.CollectibleType;
            task.Source = request.Source;

            _context.SaveChanges();

            return task.Id;
        }

        /// <summary>
        /// Sets a task's game data references.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns the ID of the updated task.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the task was not found in the database.</response>
        [HttpPut("references")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> SetGameDataReferences(SetGameDataReferencesRequest request)
        {
            var task = _context.Tasks.Find(request.TaskId);

            if (task is null)
                return NotFound();

            task.GameDataReferences = request.GameDataReferenceItems.Select(ri =>
                    new GameDataReference(ri.GameId, ri.Type, ri.Subclass, ri.Description))
                .ToList();

            _context.SaveChanges();

            return task.Id;
        }

        /// <summary>
        /// Sets a task's notes. Null or whitespace notes will be set to `null`.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns the ID of the updated task.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the task was not found in the database.</response>
        [HttpPatch("notes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> SetTaskNotes(SetTaskNotesRequest request)
        {            
            var task = _context.Tasks.Find(request.TaskId);

            if (task is null)
                return NotFound();

            task.Notes = string.IsNullOrWhiteSpace(request.Notes) ? null : request.Notes;

            _context.SaveChanges();

            return task.Id;
        }

        /// <summary>
        /// Adds a task to a user's favourites list.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns the ID of the updated task.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the task was not found in the database.</response>
        [HttpPatch("favourites/add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> AddTaskToFavourites(AddTaskToFavouritesRequest request)
        {
            var task = _context.Tasks.Find(request.TaskId);

            if (task is null)
                return NotFound();

            task.IsFavourite = true;

            _context.SaveChanges();

            return task.Id;
        }

        /// <summary>
        /// Removes a task from a user's favourites list.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns the ID of the updated task.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the task was not found in the database.</response>
        [HttpPatch("favourites/remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> RemoveTaskFromFavourites(RemoveTaskFromFavouritesRequest request)
        {
            var task = _context.Tasks.Find(request.TaskId);

            if (task is null)
                return NotFound();

            task.IsFavourite = false;

            _context.SaveChanges();

            return task.Id;
        }

        /// <summary>
        /// Removes a task.
        /// </summary>
        /// <param name="taskId">The ID of the task.</param>
        /// <response code="200">Returns the ID of the deleted task.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the task was not found in the database.</response>
        [HttpDelete("{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> DeleteTask(Guid taskId)
        {
            var task = _context.Tasks.Find(taskId);

            if (task is null)
                return NotFound();

            var taskCharacters = _context.TaskCharacters.Where(tc => tc.TaskId == taskId);

            _context.TaskCharacters.RemoveRange(taskCharacters);
            _context.SaveChanges();

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return task.Id;
        }
    }
}
