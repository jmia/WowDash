using System;
using Microsoft.AspNetCore.Mvc;
using WowDash.ApplicationCore.DTO;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;

namespace WowDash.WebUI.Controllers
{
    [Route("api/characters")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CharactersController(ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
