using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using wow_dashboard.ApplicationCore.Models.BlizzardData;

namespace wow_dashboard.WebUI.Controllers
{
    [Route("api/QuestAreas")]
    [ApiController]
    public class QuestAreasController : BaseBlizzardApiController
    {
        public QuestAreasController(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration) { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<QuestArea>>> GetQuestAreas()
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/quest/area/index?" +
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
                    var index = await JsonSerializer.DeserializeAsync<QuestAreaIndex>(await content);

                    return Ok(index.Areas.Select(a => new QuestArea { Id = a.Id, Area = a.Name }).ToList());
                }
                else
                {
                    throw new Exception("The response was status code " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled status code from 'GetQuestAreas': " + ex.Message);
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<QuestArea>> GetQuestArea(int id)
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/quest/area/" + id + 
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
                    var questArea = await JsonSerializer.DeserializeAsync<QuestArea>(await content);

                    return Ok(questArea);
                }
                else
                {
                    throw new Exception("The response was status code " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unhandled status code from 'GetQuestArea': " + ex.Message);
            }

        }
    }
}
