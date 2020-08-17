using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    /// <summary>
    /// Represents a JSON object for an item
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class Item
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("quality")]
        public ItemQuality Quality { get; set; }
        [JsonPropertyName("level")]
        public int ItemLevel { get; set; }
        [JsonPropertyName("item_class")]
        public ItemClass Class { get; set; }
        [JsonPropertyName("item_subclass")]
        public ItemSubclass Subclass { get; set; }

        /// <summary>
        /// An item's quality
        /// (e.g. rare, epic, legendary)
        /// </summary>
        public class ItemQuality
        {
            [JsonPropertyName("type")]
            public string Type { get; set; }
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        /// <summary>
        /// An item's class
        /// (e.g. battle pet, recipe, armor, weapon)
        /// </summary>
        public class ItemClass
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }

        /// <summary>
        /// An item's subclass
        /// (e.g. companion pet, mount, toy)
        /// </summary>
        public class ItemSubclass
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
        }
    }
}
