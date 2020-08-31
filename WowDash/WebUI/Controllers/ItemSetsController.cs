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
    [Route("api/item-sets")]
    [ApiController]
    public class ItemSetsController : BaseBlizzardApiController
    {
        public ItemSetsController(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ItemSet>>> GetItemSets()
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/item-set/index?" +
                "namespace=static-us&locale=en_US");

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
                    var index = await JsonSerializer.DeserializeAsync<ItemSetIndex>(await content);

                    return Ok(index.ItemSets.ToList());
                }
                else
                {
                    throw new Exception("The response was status code " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled status code from 'GetItemSets': " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ItemSet>> GetItemSet(int id)
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/item-set/" + id +
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
                    var achievement = await JsonSerializer.DeserializeAsync<ItemSet>(await content);

                    return Ok(achievement);
                }
                else
                {
                    throw new Exception("The response was status code " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled status code from 'GetItemSet': " + ex.Message);
            }
        }

        [HttpGet("search/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ItemSet>>> SearchItemSetsByName(string name)
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/item-set/index?" +
                "namespace=static-us&locale=en_US");

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
                    var index = await JsonSerializer.DeserializeAsync<ItemSetIndex>(await content);

                    var results = index.ItemSets.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

                    return Ok(results);
                }
                else
                {
                    throw new Exception("The response was status code " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled status code from 'SearchItemSetsByName': " + ex.Message);
            }
        }
    }
}
