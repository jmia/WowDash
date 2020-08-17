using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    public class Achievement
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("points")]
        public int Points { get; set; }
        [JsonPropertyName("is_account_wide")]
        public bool IsAccountWide { get; set; }
        [JsonPropertyName("criteria")]
        public AchievementCriteria Criteria { get; set; }
    }

    public class AchievementCriteria
    {
        [JsonPropertyName("child_criteria")]
        public ChildCriteria[] ChildCriteria { get; set; }
    }

    public class ChildCriteria
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
