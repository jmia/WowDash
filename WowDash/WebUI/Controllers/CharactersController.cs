using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}
