using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            // Map GameDataReference entities to the GameDataReferenceItem DTO
            var gameDataReferences = task.GameDataReferences.Select(gdr =>
                new GameDataReferenceItem(gdr.Id, gdr.GameId, gdr.Type, gdr.Subclass, gdr.Description))
                .OrderBy(gdr => gdr.Type).ToList();

            return new TaskResponse(task.Id, task.PlayerId, task.Description, gameDataReferences, task.IsFavourite,
                task.Notes, task.TaskType, task.CollectibleType, task.Source, task.Priority, task.RefreshFrequency);
        }

        /// <summary>
        /// Gets a list of unique dungeon names for filtering.
        /// </summary>
        /// <param name="playerId">The ID of the player.</param>
        /// <response code="200">Returns the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        [HttpGet("dungeon-index/{playerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ICollection<FilterListSourceResponse>> GetPlayerDungeonsList(Guid playerId)
        {
            var tasks = _context.Tasks.AsNoTracking().Where(t => t.PlayerId == playerId && 
                t.GameDataReferences.Any(gdr => gdr.Type == GameDataReference.GameDataType.Dungeon));

            var references = tasks.SelectMany(t => t.GameDataReferences);
            var distinctReferences = references.DistinctBy(r => r.GameId);

            return distinctReferences.Select(dr => new FilterListSourceResponse(dr.GameId, dr.Description)).OrderBy(r => r.Name).ToList();
        }

        /// <summary>
        /// Gets all favourite tasks for a player.
        /// </summary>
        /// <param name="playerId">The ID of the player.</param>
        /// <response code="200">Returns the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        [HttpGet("zone-index/{playerId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ICollection<FilterListSourceResponse>> GetPlayerZonesList(Guid playerId)
        {
            var tasks = _context.Tasks.AsNoTracking().Where(t => t.PlayerId == playerId &&
                t.GameDataReferences.Any(gdr => gdr.Type == GameDataReference.GameDataType.Zone));

            var references = tasks.SelectMany(t => t.GameDataReferences);
            var distinctReferences = references.DistinctBy(r => r.GameId);

            return distinctReferences.Select(dr => new FilterListSourceResponse(dr.GameId, dr.Description)).OrderBy(r => r.Name).ToList();
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
            var tasks = _context.Tasks.AsNoTracking().Where(t => t.PlayerId == filterModel.PlayerId);

            ApplyFilters(ref tasks, filterModel);

            ApplySort(ref tasks, filterModel);

            var taskList = new List<TaskResponse>();

            foreach (var task in tasks)
            {
                var gameDataReferences = task.GameDataReferences.Select(gdr =>
                    new GameDataReferenceItem(gdr.Id, gdr.GameId, gdr.Type, gdr.Subclass, gdr.Description))
                    .OrderBy(gdr => gdr.Type).ToList();
                
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
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<Guid> InitializeTask(InitializeTaskRequest request)
        //{
        //    var player = _context.Players.Find(request.PlayerId);

        //    if (player is null)
        //        return NotFound();

        //    var task = new Task(player.Id, request.TaskType);

        //    _context.Tasks.Add(task);
        //    _context.SaveChanges();

        //    return task.Id;
        //}

        /// <summary>
        /// The second biggest cop-out method of all time.
        /// One day I will have a beautiful task-based UI that will
        /// use all the beautiful commands I split up and tested
        /// so lovingly.
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> AddTask(AddTaskRequest request)
        {
            var player = _context.Players.Find(request.PlayerId);

            if (player is null)
                return NotFound();

            var task = new Task(request.PlayerId, request.TaskType)
            {
                Description = request.Description,
                IsFavourite = request.IsFavourite,
                Notes = request.Notes,
                CollectibleType = request.CollectibleType,
                Source = request.Source,
                Priority = request.Priority,
                RefreshFrequency = request.RefreshFrequency
            };

            task.GameDataReferences = request.GameDataReferenceItems.Select(ri =>
                    new GameDataReference(ri.GameId, ri.Type, ri.Subclass, ri.Description))
                .ToList();

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return task.Id;
        }

        /// <summary>
        /// The biggest cop-out method of all time.
        /// </summary>
        /// <param name="request"></param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> UpdateTask(UpdateTaskRequest request)
        {
            var task = _context.Tasks.Find(request.TaskId);

            if (task is null)
                return NotFound();

            task.TaskType = request.TaskType;
            task.Description = request.Description;
            task.IsFavourite = request.IsFavourite;
            task.Notes = request.Notes;
            task.CollectibleType = request.CollectibleType;
            task.Source = request.Source;
            task.Priority = request.Priority;
            task.RefreshFrequency = request.RefreshFrequency;

            task.GameDataReferences = request.GameDataReferenceItems.Select(ri =>
                    new GameDataReference(ri.GameId, ri.Type, ri.Subclass, ri.Description))
                .ToList();

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
            task.RefreshFrequency = request.RefreshFrequency;

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

        /// <summary>
        /// Filter a list of tasks by model properties.
        /// </summary>
        /// <param name="tasks">The list of tasks to filter.</param>
        /// <param name="filterModel">The key-values for filtering.</param>
        internal void ApplyFilters(ref IQueryable<Task> tasks, FilterModel filterModel)
        {
            // Don't Panic.

            // IsFavourite
            if (filterModel.IsFavourite)
            {
                tasks = tasks.Where(t => t.IsFavourite == true);
            }

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

            // CharacterID
            if (!string.IsNullOrWhiteSpace(filterModel.CharacterId))
            {
                var characterIdStrings = filterModel.CharacterId.Split('|');
                if (characterIdStrings.Count() > 0)
                {
                    // Try parsing every string to a GUID
                    Guid characterId = Guid.Empty;
                    var characterIds = characterIdStrings.Where(c => Guid.TryParse(c, out characterId))
                                                         .Select(x => characterId).ToList();

                    // Get all TaskCharacters that have CharacterIds that match the supplied characterId list
                    var taskCharacters = _context.TaskCharacters.Where(tc => characterIds.Contains(tc.CharacterId));

                    // Filter to all tasks that match these TaskCharacters
                    tasks = tasks.Where(t => taskCharacters.Any(tc => tc.TaskId == t.Id)).Distinct();
                }
            }

            // DungeonID
            if (!string.IsNullOrWhiteSpace(filterModel.DungeonId))
            {
                var dungeonIdStrings = filterModel.DungeonId.Split('|');
                if (dungeonIdStrings.Count() > 0)
                {
                    // Try parsing every string to an int
                    int dungeonId = 0;
                    var dungeonIds = dungeonIdStrings.Where(c => int.TryParse(c, out dungeonId))
                                                         .Select(x => dungeonId).ToList();

                    // Filter to all tasks that have game references that are dungeons and match the supplied dungeonId list
                    tasks = tasks.Where(t => t.GameDataReferences
                        // This is like 3 loops? Help?
                        .Any(gdr => gdr.GameId != null && gdr.Type == GameDataReference.GameDataType.Dungeon && 
                            dungeonIds.Contains((int)gdr.GameId)));
                }
            }

            // ZoneID
            if (!string.IsNullOrWhiteSpace(filterModel.ZoneId))
            {
                var zoneIdStrings = filterModel.ZoneId.Split('|');
                if (zoneIdStrings.Count() > 0)
                {
                    // Try parsing every string to an int
                    int zoneId = 0;
                    var zoneIds = zoneIdStrings.Where(c => int.TryParse(c, out zoneId))
                                                         .Select(x => zoneId).ToList();

                    // Filter to all tasks that have game references that are zones and match the supplied zoneId list
                    tasks = tasks.Where(t => t.GameDataReferences
                        .Any(gdr => gdr.GameId != null && gdr.Type == GameDataReference.GameDataType.Zone &&
                            zoneIds.Contains((int)gdr.GameId)));
                }
            }

            // If only where there are active character attempts left
            if (filterModel.OnlyActiveAttempts)
            {
                var taskChars = _context.TaskCharacters.Where(tc => tc.IsActive == true).Select(tc => tc.TaskId).ToList().Distinct();
                tasks = tasks.Where(t => taskChars.Contains(t.Id));
            }
        }

        /// <summary>
        /// Sort a list of tasks by priority or alphabetical by description.
        /// </summary>
        /// <param name="tasks">The list of tasks to sort.</param>
        /// <param name="filterModel">The key-values for sorting.</param>
        internal void ApplySort(ref IQueryable<Task> tasks, FilterModel filterModel)
        {
            if (!string.IsNullOrWhiteSpace(filterModel.SortBy))
            {
                switch (filterModel.SortBy)
                {
                    case "priority_asc":
                        tasks = tasks.OrderBy(t => t.Priority).ThenBy(t => t.Description);
                        break;
                    case "alpha_asc":
                        tasks = tasks.OrderBy(t => t.Description).ThenByDescending(t => t.Priority);
                        break;
                    case "alpha_desc":
                        tasks = tasks.OrderByDescending(t => t.Description).ThenByDescending(t => t.Priority);
                        break;
                    case "priority_desc":
                    default:
                        tasks = tasks.OrderByDescending(t => t.Priority).ThenBy(t => t.Description);
                        break;
                }
            }
            else
            {
                // Is this what I actually want as default?
                tasks = tasks.OrderByDescending(t => t.Priority).ThenBy(t => t.Description);
            }
        }
    }
}
