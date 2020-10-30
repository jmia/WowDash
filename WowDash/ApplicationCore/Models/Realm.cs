using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    /// <summary>
    /// Represents a JSON object for a realm
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class Realm
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }
    }
}
