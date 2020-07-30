using System.Text.Json.Serialization;

namespace wow_dashboard.Models.BlizzardData
{
    public class AccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }
    }
}
