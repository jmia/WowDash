using System.Text.Json.Serialization;

namespace wow_dashboard.ApplicationCore.Models.BlizzardData
{
    public class AccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }
    }
}
