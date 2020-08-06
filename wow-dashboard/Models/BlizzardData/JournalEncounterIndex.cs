using System.Text.Json.Serialization;

namespace wow_dashboard.Models.BlizzardData
{
    /// <summary>
    /// Represents a JSON object for all boss fights
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class JournalEncounterIndex
    {
        [JsonPropertyName("encounters")]
        public JournalEncounter[] Encounters { get; set; }
    }
}
