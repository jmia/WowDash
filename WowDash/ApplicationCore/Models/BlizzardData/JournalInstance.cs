using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models.BlizzardData
{
    /// <summary>
    /// Represents a JSON object for a dungeon
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class JournalInstance
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("encounters")]
        public Encounter[] Encounters { get; set; }
        [JsonPropertyName("modes")]
        public ModeWrapper[] Modes { get; set; }

        public class Encounter
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        public class ModeWrapper
        {
            [JsonPropertyName("mode")]
            public Mode Mode { get; set; }
        }

        public class Mode
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }
    }

}
