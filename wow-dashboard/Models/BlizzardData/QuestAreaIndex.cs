using System.Text.Json.Serialization;

namespace wow_dashboard.Models.BlizzardData
{
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
