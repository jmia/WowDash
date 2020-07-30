using System.Text.Json.Serialization;

namespace wow_dashboard.Models.BlizzardData
{

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
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("id")]
            public int Id { get; set; }
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
