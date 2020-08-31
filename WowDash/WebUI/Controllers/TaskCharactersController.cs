using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WowDash.ApplicationCore.DTO;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;

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
    }
}
