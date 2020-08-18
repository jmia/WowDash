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
    [Route("api/Items")]
    [ApiController]
    public class ItemsController : BaseBlizzardApiController
    {
        public ItemsController(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration) { }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

            try
            {
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
                else
                {
                    throw new Exception("The response was status code " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled status code from 'GetItem': " + ex.Message);
            }
        }

        [HttpGet("search/item/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

            try
            {
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
                else
                {
                    throw new Exception("The response was status code " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled status code from 'SearchItemsByItemName': " + ex.Message);
            }
        }
    }
}
