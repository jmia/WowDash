using System.Text.Json.Serialization;

namespace wow_dashboard.Models.BlizzardData
{

    public class JournalEncounter
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        public ItemWrapper[] Items { get; set; }
        public Instance Instance { get; set; }
    }

    public class Instance
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class ItemWrapper
    {
        [JsonPropertyName("item")]
        public Item Item { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
