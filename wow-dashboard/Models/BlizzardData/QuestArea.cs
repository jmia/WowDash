using System.Text.Json.Serialization;

namespace wow_dashboard.Models.BlizzardData
{
    public class QuestArea
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("area")]
        public string Area { get; set; }
    }
}
