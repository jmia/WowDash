using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models.BlizzardData
{
    /// <summary>
    /// Represents a JSON object for a collection of boss fights
    /// returned from a Blizzard API search.
    /// </summary>
    public class JournalEncounterSearchResult
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        [JsonPropertyName("maxPageSize")]
        public int MaxPageSize { get; set; }
        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }
        [JsonPropertyName("results")]
        public Result[] Results { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public Name Name { get; set; }
    }

    public class Name
    {
        public string en_US { get; set; }
    }

}
