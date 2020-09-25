using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WowDash.ApplicationCore.Models;

namespace WowDash.WebUI.Controllers
{
    [Route("api/achievements")]
    [Produces("application/json")]
    [ApiController]
    public class AchievementsController : BaseBlizzardApiController
    {
        public AchievementsController(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration) { }

        /// <summary>
        /// Get an achievement by its game ID.
        /// </summary>
        /// <param name="id">The game ID of the achievement.</param>
        /// <response code="200">Returns the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="401">If the client failed to authorize an access token.</response>
        /// <response code="404">If the resource was not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Achievement>> GetAchievement(int id)
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/achievement/" + id +
                "?namespace=static-us&locale=en_US");

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {access_token}");

            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return Unauthorized();

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStreamAsync();
                var achievement = await JsonSerializer.DeserializeAsync<Achievement>(await content);

                return Ok(achievement);
            }

            return StatusCode((int)response.StatusCode);
        }

        /// <summary>
        /// Search for an achievement by its name (partials accepted, case insensitive).
        /// </summary>
        /// <param name="name">The search term.</param>
        /// <response code="200">Returns a list of matched resources.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="401">If the client failed to authorize an access token.</response>
        /// <response code="404">If the resource was not found.</response>
        [HttpGet("search/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SearchResult>>> SearchAchievementsByName(string name)
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/achievement/index?" +
                "namespace=static-us&locale=en_US");

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {access_token}");

            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return Unauthorized();

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStreamAsync();
                var index = await JsonSerializer.DeserializeAsync<AchievementIndex>(await content);

                var results = index.Achievements.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .Take(50)
                    .Select(a => new SearchResult() { Id = a.Id, Name = a.Name })
                    .ToList();

                return Ok(results);
            }

            return StatusCode((int)response.StatusCode);
        }
    }
}
