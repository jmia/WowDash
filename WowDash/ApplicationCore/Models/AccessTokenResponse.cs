using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    public class AccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }
    }
}
