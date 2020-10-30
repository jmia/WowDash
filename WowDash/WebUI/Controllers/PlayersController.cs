using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WowDash.ApplicationCore.DTO.Requests;
using WowDash.ApplicationCore.DTO.Responses;
using WowDash.Infrastructure;

namespace WowDash.WebUI.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a player's profile.
        /// </summary>
        /// <param name="id">The ID of the player.</param>
        /// <response code="200">Returns the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the resource is not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<GetPlayerProfileResponse> GetPlayerProfile(Guid id)
        {
            var player = _context.Players.Find(id);

            if (player is null)
                return NotFound();

            return new GetPlayerProfileResponse(player.Id, player.DisplayName, player.DefaultTaskType, player.DefaultRealm);
        }

        /// <summary>
        /// Gets a player's profile.
        /// </summary>
        /// <param name="request">The collection of values to update on the profile (display name and defaults).</param>
        /// <response code="200">Returns the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="404">If the resource is not found.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> SetPlayerProfile(SetPlayerProfileRequest request)
        {
            var player = _context.Players.Find(request.PlayerId);

            if (player is null)
                return NotFound();

            player.DisplayName = request.DisplayName;
            player.DefaultRealm = request.DefaultRealm;
            player.DefaultTaskType = request.DefaultTaskType;

            _context.SaveChanges();

            return player.Id;
        }
    }
}
