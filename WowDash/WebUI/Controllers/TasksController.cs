using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WowDash.ApplicationCore.DTO.Common;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.ApplicationCore.DTO.Responses;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;
using static WowDash.ApplicationCore.Common.Enums;

[assembly: InternalsVisibleTo("WowDash.UnitTests")]
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
        /// Gets a task by its ID.
        /// </summary>
        /// <param name="taskId">The ID of the task.</param>
        /// <response code="200">Returns the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the resource was not found in the database.</response>
        [HttpGet("{taskId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TaskResponse> GetTaskById(Guid taskId)
        {
            var task = _context.Tasks.Find(taskId);

            if (task is null)
                return NotFound();

            var gameDataReferences = task.GameDataReferences.Select(gdr =>
                new GameDataReferenceItem(gdr.Id, gdr.GameId, gdr.Type, gdr.Subclass, gdr.Description))
                .ToList();

            return new TaskResponse(task.Id, task.PlayerId, task.Description, gameDataReferences, task.IsFavourite,
                task.Notes, task.TaskType, task.CollectibleType, task.Source, task.Priority, task.RefreshFrequency);
        }

        /// <summary>
        /// Gets all favourite tasks for a player.
        /// </summary>
        /// <param name="playerId">The ID of the player.</param>
        /// <response code="200">Returns the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        [HttpGet("favourites/{playerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GetFavouriteTasksResponse> GetFavouriteTasks(Guid playerId)
        {
            var tasks = _context.Tasks.Where(t => t.PlayerId == playerId && t.IsFavourite == true);

            var taskList = new List<TaskResponse>();

            foreach (var task in tasks)
            {
                var gameDataReferences = task.GameDataReferences.Select(gdr =>
                    new GameDataReferenceItem(gdr.Id, gdr.GameId, gdr.Type, gdr.Subclass, gdr.Description))
                    .ToList();

                taskList.Add(new TaskResponse(task.Id, task.PlayerId, task.Description, gameDataReferences, task.IsFavourite,
                    task.Notes, task.TaskType, task.CollectibleType, task.Source, task.Priority, task.RefreshFrequency));
            }

            return new GetFavouriteTasksResponse(playerId, taskList);
        }

        /// <summary>
        /// Gets all tasks for a player with a given filter.
        /// </summary>
        /// <param name="filterModel">A collection of properties on which to filter the list.</param>
        /// <response code="200">Returns the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<GetTasksResponse> GetTasks([FromQuery] FilterModel filterModel)
        {
            var tasks = _context.Tasks.Where(t => t.PlayerId == filterModel.PlayerId);

            ApplyFilters(ref tasks, filterModel);

            var taskList = new List<TaskResponse>();

            foreach (var task in tasks)
            {
                var gameDataReferences = task.GameDataReferences.Select(gdr =>
                    new GameDataReferenceItem(gdr.Id, gdr.GameId, gdr.Type, gdr.Subclass, gdr.Description))
                    .ToList();
                
                taskList.Add(new TaskResponse(task.Id, task.PlayerId, task.Description, gameDataReferences, task.IsFavourite,
                    task.Notes, task.TaskType, task.CollectibleType, task.Source, task.Priority, task.RefreshFrequency));
            }

            return new GetTasksResponse(filterModel.PlayerId, taskList);
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns the ID of the created task.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the player was not found in the database.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        internal void ApplyFilters(ref IQueryable<Task> tasks, FilterModel filterModel)
        {
            //public string CharacterId { get; set; }
            //public string DungeonId { get; set; }
            //public string ZoneId { get; set; }

            // TaskType
            if (!string.IsNullOrWhiteSpace(filterModel.TaskType))
            {
                // Creates array with up to 3 elements (0, 1, 2)
                var taskTypes = filterModel.TaskType.Split('|').Select(tt => int.Parse(tt)).ToList();

                // Filters the list with OR clauses
                switch (taskTypes.Count)
                {
                    case 1:
                        tasks = tasks.Where(t => t.TaskType == (TaskType)taskTypes[0]);
                        break;
                    case 2:
                        tasks = tasks.Where(t => t.TaskType == (TaskType)taskTypes[0] || 
                            t.TaskType == (TaskType)taskTypes[1]);
                        break;
                    default:
                        // If it has 3 or 0, we're sending back everything
                        break;
                }
            }

            // RefreshFrequency
            if (!string.IsNullOrWhiteSpace(filterModel.RefreshFrequency))
            {
                // Creates array with up to 3 elements (0, 1, 2)
                var refreshFrequencies = filterModel.RefreshFrequency.Split('|').Select(tt => int.Parse(tt)).ToList();

                // Filters the list with OR clauses
                switch (refreshFrequencies.Count)
                {
                    case 1:
                        tasks = tasks.Where(t => t.RefreshFrequency == (RefreshFrequency)refreshFrequencies[0]);
                        break;
                    case 2:
                        tasks = tasks.Where(t => t.RefreshFrequency == (RefreshFrequency)refreshFrequencies[0] || 
                            t.RefreshFrequency == (RefreshFrequency)refreshFrequencies[1]);
                        break;
                    default:
                        // If it has 3 or 0, we're sending back everything
                        break;
                }
            }

            // CollectibleType
            if (!string.IsNullOrWhiteSpace(filterModel.CollectibleType))
            {
                // Creates array with up to 4 elements (0, 1, 2, 3)
                var collectibleTypes = filterModel.CollectibleType.Split('|').Select(tt => int.Parse(tt)).ToList();

                // Filters the list with OR clauses
                switch (collectibleTypes.Count)
                {
                    case 1:
                        tasks = tasks.Where(t => t.CollectibleType == (CollectibleType)collectibleTypes[0]);
                        break;
                    case 2:
                        tasks = tasks.Where(t => t.CollectibleType == (CollectibleType)collectibleTypes[0] ||
                            t.CollectibleType == (CollectibleType)collectibleTypes[1]);
                        break;
                    case 3:
                        tasks = tasks.Where(t => t.CollectibleType == (CollectibleType)collectibleTypes[0] ||
                            t.CollectibleType == (CollectibleType)collectibleTypes[1] ||
                            t.CollectibleType == (CollectibleType)collectibleTypes[2]);
                        break;
                    default:
                        // If it has 4 or 0, we're sending back everything
                        break;
                }
            }

            // If any CharacterIDs are specified
            if (!string.IsNullOrWhiteSpace(filterModel.CharacterId))
            {
                var characterIdStrings = filterModel.CharacterId.Split('|');
                if (characterIdStrings.Count() > 0)
                {
                    // Parse a list of all chars I want to sort on
                    Guid characterId = Guid.Empty;
                    var characterIds = characterIdStrings.Where(c => Guid.TryParse(c, out characterId))
                                                         .Select(x => characterId).ToList();

                    // I need a list of all tasks that have any of these characters as an assigned TaskCharacter


                    // Get all task characters that have these parsed character IDs
                    // And filter the list of tasks to any that match the task IDs of the task characters

                    // OR get all task characters that have any of the task IDs from the passed in collection
                    // And filter the list of tasks to any that match the character IDs of the parsed character IDs

                    // "WHERE characterId = this OR characterId = that OR characterId = thisotherthing"

                }
            }

        }
    }
}
