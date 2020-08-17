using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    public class AchievementIndex
    {
        [JsonPropertyName("achievements")]
        public Achievement[] Achievements { get; set; }
    }
}
