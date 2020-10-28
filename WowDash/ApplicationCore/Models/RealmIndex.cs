using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    /// <summary>
    /// Represents a JSON object for a collection of realms
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class RealmIndex
    {
        [JsonPropertyName("realms")]
        public Realm[] Realms { get; set; }
    }
}
