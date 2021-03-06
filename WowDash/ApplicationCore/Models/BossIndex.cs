﻿using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    /// <summary>
    /// Represents a JSON object for all boss fights
    /// returned from a Blizzard API GET request.
    /// </summary>
    public class BossIndex
    {
        [JsonPropertyName("encounters")]
        public Boss[] Encounters { get; set; }
    }
}
