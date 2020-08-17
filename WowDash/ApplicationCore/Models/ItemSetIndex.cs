using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    /// <summary>
    /// Represents a JSON object for all item sets
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class ItemSetIndex
    {
        [JsonPropertyName("item_sets")]
        public ItemSet[] ItemSets { get; set; }
    }

}
