using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models.BlizzardData
{
    /// <summary>
    /// Represents a JSON object for a zone
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class QuestArea
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("area")]
        public string Area { get; set; }
    }
}
