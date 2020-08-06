using System.Text.Json.Serialization;

namespace wow_dashboard.Models.BlizzardData
{
    public class JournalEncounterIndex
    {
        [JsonPropertyName("encounters")]
        public JournalEncounter[] Encounters { get; set; }
    }
}
