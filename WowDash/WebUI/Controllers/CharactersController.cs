using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;

namespace WowDash.WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/characters")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CharactersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("all/{playerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ICollection<Character>> GetPlayerCharacters(Guid playerId)
        {
            var characters = _context.Characters.Where(c => c.PlayerId == playerId);

            if (characters is null) // is empty?
                return NotFound();

            return characters.ToList();
        }

        [HttpGet("{characterId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Character> GetCharacterById(Guid characterId)
        {
            var character = _context.Characters.Find(characterId);

            if (character is null)
                return NotFound();

            return character;
        }

        /// <summary>
        /// Creates a new character.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns the ID of the created character.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the player was not found in the database.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Guid> AddCharacter(AddCharacterRequest request)
        {
            var player = _context.Players.Find(request.PlayerId);

            if (player is null)
                return NotFound();

            var character = new Character(player.Id, request.GameId, request.Name, request.Gender, request.Level,
                request.Class, request.Race, request.Realm);

            _context.Characters.Add(character);
            _context.SaveChanges();

            return character.Id;
        }

        /// <summary>
        /// Updates all of a character's properties.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="200">Returns the ID of the updated characters.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the character was not found in the database.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> UpdateCharacter(UpdateCharacterRequest request)
        {
            var character = _context.Characters.Find(request.CharacterId);

            if (character is null)
                return NotFound();

            character.GameId = request.GameId;
            character.Name = request.Name;
            character.Gender = request.Gender;
            character.Level = request.Level;
            character.Class = request.Class;
            character.Race = request.Race;
            character.Realm = request.Realm;

            _context.SaveChanges();

            return character.Id;
        }

        /// <summary>
        /// Deletes a character from the database.
        /// </summary>
        /// <param name="characterId">The ID of the character.</param>
        /// <response code="200">Returns the ID of the deleted character.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the character was not found in the database.</response>
        [HttpDelete("{characterId}")]
        public ActionResult<Guid> DeleteCharacter(Guid characterId)
        {
            var character = _context.Characters.Find(characterId);

            if (character is null)
                return NotFound();

            var taskCharacters = _context.TaskCharacters.Where(tc => tc.CharacterId == characterId);

            _context.TaskCharacters.RemoveRange(taskCharacters);
            _context.SaveChanges();

            _context.Characters.Remove(character);
            _context.SaveChanges();

            return character.Id;
        }
    }
}
