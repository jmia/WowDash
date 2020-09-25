using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    /// <summary>
    /// Represents a JSON object for all dungeons
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class DungeonIndex
    {
        [JsonPropertyName("instances")]
        public Dungeon[] Dungeons { get; set; }
    }

}
