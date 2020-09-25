using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    public class SearchResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
