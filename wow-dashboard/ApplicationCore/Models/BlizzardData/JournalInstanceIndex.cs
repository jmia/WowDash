using System.Text.Json.Serialization;

namespace wow_dashboard.ApplicationCore.Models.BlizzardData
{
    /// <summary>
    /// Represents a JSON object for all dungeons
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class JournalInstanceIndex
    {
        [JsonPropertyName("instances")]
        public JournalInstance[] Instances { get; set; }
    }

}
