using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    /// <summary>
    /// Represents a JSON object for a boss fight
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class JournalEncounter
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("items")]
        public ItemWrapper[] Items { get; set; }
        [JsonPropertyName("instance")]
        public JournalInstance Instance { get; set; }

        /// <summary>
        /// A wrapper class for a collection of items.
        /// These return objects are nested and can't be accessed
        /// by calling Item[].
        /// </summary>
        public class ItemWrapper
        {
            [JsonPropertyName("item")]
            public Item Item { get; set; }
        }
    }
}
