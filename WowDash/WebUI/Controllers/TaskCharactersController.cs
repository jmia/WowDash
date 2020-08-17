using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;

namespace WowDash.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCharactersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskCharactersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskCharacters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskCharacter>>> GetTaskCharacters()
        {
            return await _context.TaskCharacters.ToListAsync();
        }

        // GET: api/TaskCharacters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskCharacter>> GetTaskCharacter(Guid id)
        {
            var taskCharacter = await _context.TaskCharacters.FindAsync(id);

            if (taskCharacter == null)
            {
                return NotFound();
            }

            return taskCharacter;
        }

        // PUT: api/TaskCharacters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskCharacter(Guid id, TaskCharacter taskCharacter)
        {
            if (id != taskCharacter.CharacterId)
            {
                return BadRequest();
            }

            _context.Entry(taskCharacter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskCharacterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskCharacters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TaskCharacter>> PostTaskCharacter(TaskCharacter taskCharacter)
        {
            _context.TaskCharacters.Add(taskCharacter);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TaskCharacterExists(taskCharacter.CharacterId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTaskCharacter", new { id = taskCharacter.CharacterId }, taskCharacter);
        }

        // DELETE: api/TaskCharacters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskCharacter>> DeleteTaskCharacter(Guid id)
        {
            var taskCharacter = await _context.TaskCharacters.FindAsync(id);
            if (taskCharacter == null)
            {
                return NotFound();
            }

            _context.TaskCharacters.Remove(taskCharacter);
            await _context.SaveChangesAsync();

            return taskCharacter;
        }

        private bool TaskCharacterExists(Guid id)
        {
            return _context.TaskCharacters.Any(e => e.CharacterId == id);
        }
    }
}
