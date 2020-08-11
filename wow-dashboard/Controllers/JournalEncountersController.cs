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
    public class JournalEncountersController : BaseBlizzardApiController
    {
        public JournalEncountersController(IHttpClientFactory clientFactory, IConfiguration configuration) 
            : base(clientFactory, configuration) { }

        [HttpGet("{id}")]
        public async Task<ActionResult<JournalEncounter>> GetJournalEncounter(int id)
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/journal-encounter/" + id + 
                "?namespace=static-us&locale=en_US");

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {access_token}");

            var response = await client.SendAsync(request);

            try
            {
                if (response != null)
                {
                    var content = response.Content.ReadAsStreamAsync();
                    var journalEncounter = await JsonSerializer.DeserializeAsync<JournalEncounter>(await content);

                    return journalEncounter;
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
        public async Task<ActionResult<IEnumerable<JournalEncounter>>> GetJournalEncounters()
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/journal-encounter/index?" +
                "namespace=static-us&locale=en_US");

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {access_token}");

            var response = await client.SendAsync(request);

            try
            {
                if (response != null)
                {
                    var content = response.Content.ReadAsStreamAsync();

                    var index = await JsonSerializer.DeserializeAsync<JournalEncounterIndex>(await content);

                    return index.Encounters.ToList();
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


        [HttpGet("search/instance/{name}")]
        public async Task<ActionResult<IEnumerable<JournalEncounter>>> SearchJournalEncountersByInstanceName(string name)
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/search/journal-encounter?" +
                "namespace=static-us&locale=en_US&instance.name.en_US=" + name +
                "&orderby=instance.name.en_US&_pageSize=50&_page=1");

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {access_token}");

            var response = await client.SendAsync(request);

            try
            {
                if (response != null)
                {
                    var content = response.Content.ReadAsStreamAsync();

                    var searchResult = await JsonSerializer.DeserializeAsync<JournalEncounterSearchResult>(await content);

                    return searchResult.Results.Select(r => new JournalEncounter { Id = r.Data.Id, Name = r.Data.Name.en_US })
                                               .ToList();
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

        [HttpGet("search/boss/{name}")]
        public async Task<ActionResult<IEnumerable<JournalEncounter>>> SearchJournalEncountersByBossName(string name)
        {
            var client = _clientFactory.CreateClient();

            var access_token = await GetAccessTokenAsync();

            using var request = new HttpRequestMessage(new HttpMethod("GET"),
                "https://us.api.blizzard.com/data/wow/search/journal-encounter?" +
                "namespace=static-us&locale=en_US&name.en_US=" + name +
                "&orderby=instance.name.en_US&_pageSize=50&_page=1");

            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {access_token}");

            var response = await client.SendAsync(request);

            try
            {
                if (response != null)
                {
                    var content = response.Content.ReadAsStreamAsync();

                    var searchResult = await JsonSerializer.DeserializeAsync<JournalEncounterSearchResult>(await content);

                    return searchResult.Results.Select(r => new JournalEncounter { Id = r.Data.Id, Name = r.Data.Name.en_US })
                                               .ToList();
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
