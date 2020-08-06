using System.Text.Json.Serialization;

namespace wow_dashboard.Models.BlizzardData
{

    // TODO - Could make this more generic
    // Or could rename the classes to be endpoint specific and keep the mapped JsonPropertyNames
    // Depends on how much data I need

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
