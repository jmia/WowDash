using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    /// <summary>
    /// Represents a JSON object for a dungeon gear set
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class ItemSet
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("items")]
        public Item[] Items { get; set; }
    }
}
