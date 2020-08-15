using System.Text.Json.Serialization;

namespace wow_dashboard.ApplicationCore.Models.BlizzardData
{
    /// <summary>
    /// Represents a JSON object for all zones
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class QuestAreaIndex
    {
        [JsonPropertyName("areas")]
        public Area[] Areas { get; set; }
    }

    public class Area
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

}
