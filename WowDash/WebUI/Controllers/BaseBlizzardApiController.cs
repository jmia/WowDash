using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WowDash.ApplicationCore.Models.BlizzardData;

namespace WowDash.WebUI.Controllers
{
    public class BaseBlizzardApiController : ControllerBase
    {
        internal readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public BaseBlizzardApiController(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [HttpGet]
        internal async Task<string> GetAccessTokenAsync()
        {
            var client = _clientFactory.CreateClient();

            var clientId = _configuration["BlizzardClientId"];
            var clientSecret = _configuration["BlizzardClientSecret"];

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
