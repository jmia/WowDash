using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    /// <summary>
    /// Represents a JSON object for a zone
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class QuestArea
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("area")]
        public string Area { set => Name = value; } // This property needs to be redirected when it comes in for naming convention.
    }
}
