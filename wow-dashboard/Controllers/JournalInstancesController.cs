using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using wow_dashboard.Models.BlizzardData;

namespace wow_dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalInstancesController : BaseBlizzardApiController
    {
        public JournalInstancesController(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration) { }

        [HttpGet("{id}")]
        public async Task<ActionResult<JournalInstance>> GetJournalInstance(int id)
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/journal-instance/" + id +
                "?namespace=static-us&locale=en_US");

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {access_token}");

            var response = await client.SendAsync(request);

            try
            {
                if (response != null)
                {
                    var content = response.Content.ReadAsStreamAsync();
                    var journalInstance = await JsonSerializer.DeserializeAsync<JournalInstance>(await content);

                    return journalInstance;
                }
                else
                {
                    throw new Exception("The response was empty.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error with your request: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JournalInstance>>> GetJournalInstances()
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/journal-instance/index?" +
                "namespace=static-us&locale=en_US");

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {access_token}");

            var response = await client.SendAsync(request);

            try
            {
                if (response != null)
                {
                    var content = response.Content.ReadAsStreamAsync();

                    var index = await JsonSerializer.DeserializeAsync<JournalInstanceIndex>(await content);

                    return index.Instances.ToList();
                }
                else
                {
                    throw new Exception("The response was empty.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error with your request: " + ex.Message);
            }
        }
    }
}
