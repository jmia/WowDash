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
    [Route("api/realms")]
    [ApiController]
    public class RealmsController : BaseBlizzardApiController
    {
        public RealmsController(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration) { }

        /// <summary>
        /// Get a realm by its game ID.
        /// </summary>
        /// <param name="id">The game ID of the zone.</param>
        /// <response code="200">Returns the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="401">If the client failed to authorize an access token.</response>
        /// <response code="404">If the resource was not found.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Realm>>> GetRealmIndex()
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/realm/index?namespace=dynamic-us&locale=en_US");

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {access_token}");

            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return Unauthorized();

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStreamAsync();
                var index = await JsonSerializer.DeserializeAsync<RealmIndex>(await content);

                return Ok(index.Realms.OrderBy(r => r.Name).ToList());
            }

            return StatusCode((int)response.StatusCode);
        }
    }
}
