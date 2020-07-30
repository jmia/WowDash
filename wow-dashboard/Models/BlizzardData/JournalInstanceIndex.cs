using System.Text.Json.Serialization;

namespace wow_dashboard.Models.BlizzardData
{

    public class JournalInstanceIndex
    {
        [JsonPropertyName("instances")]
        public JournalInstance[] Instances { get; set; }
    }

}
