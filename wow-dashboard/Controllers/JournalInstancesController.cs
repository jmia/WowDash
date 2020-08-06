using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wow_dashboard.Models.BlizzardData;

namespace wow_dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalInstancesController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpGet("{id}")]
        public async Task<ActionResult<JournalInstance>> GetJournalInstance(int id)
        {
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

        internal async Task<string> GetAccessTokenAsync()
        {

            var clientId = "";
            var clientSecret = "";

            using var request = new HttpRequestMessage(new HttpMethod("POST"), "https://us.battle.net/oauth/token");

            var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));
            
            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");

            request.Content = new StringContent("grant_type=client_credentials");
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            try
            {
                var response = await client.SendAsync(request);

                if (response != null)
                {
                    var content = response.Content.ReadAsStreamAsync();
                    var tokenResponse = await JsonSerializer.DeserializeAsync<AccessTokenResponse>(await content);

                    return tokenResponse.Token;
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
