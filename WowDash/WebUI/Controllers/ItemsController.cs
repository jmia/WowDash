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
    [Route("api/items")]
    [ApiController]
    public class ItemsController : BaseBlizzardApiController
    {
        public ItemsController(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration) { }

        /// <summary>
        /// Get an item by its game ID.
        /// </summary>
        /// <param name="id">The game ID of the item.</param>
        /// <response code="200">Returns the resource.</response>
        /// <response code="400">If the request is null or missing required fields.</response>
        /// <response code="401">If the client failed to authorize an access token.</response>
        /// <response code="404">If the resource was not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/item/" + id +
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
                var item = await JsonSerializer.DeserializeAsync<Item>(await content);

                return Ok(item);
            }

            return StatusCode((int)response.StatusCode);
        }

        /// <summary>
        /// Search for an item by its name (partials accepted, case insensitive).
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
        public async Task<ActionResult<IEnumerable<Item>>> SearchItemsByItemName(string name)
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/search/item?" +
                "namespace=static-us&locale=en_US&name.en_US=" + name +
                "&orderby=instance.name.en_US&_pageSize=50&_page=1");

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {access_token}");

            var response = await client.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return Unauthorized();

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStreamAsync();
                var searchResult = await JsonSerializer.DeserializeAsync<BlizzardSearchResult>(await content);

                return Ok(searchResult.Results.Select(r => new Item { Id = r.Data.Id, Name = r.Data.Name.en_US })
                                            .ToList());
            }

            return StatusCode((int)response.StatusCode);
        }
    }
}
