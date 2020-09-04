using System;
using System.Text.Json.Serialization;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.IntegrationTests.Responses
{
    public class GetPlayerResponse
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
