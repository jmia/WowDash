using System;
using System.Text.Json.Serialization;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.IntegrationTests.Responses
{
    /// <summary>
    /// Represents a JSON response for a sample player model
    /// from the internal API that would be received by a
    /// front end caller. It's mapped to an object for 
    /// ease of testing.
    /// </summary>
    public class GetPlayerResponse      // TODO: Delete this when queries are built for this endpoint
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("googleId")]
        public string GoogleId { get; set; }
        [JsonPropertyName("blizzardId")]
        public string BlizzardId { get; set; }
        [JsonPropertyName("defaultTaskType")]
        public TaskType DefaultTaskType { get; set; }
        [JsonPropertyName("defaultRealm")]
        public string DefaultRealm { get; set; }
    }
}
