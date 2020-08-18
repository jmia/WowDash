using System.Text.Json.Serialization;

namespace WowDash.ApplicationCore.Models
{
    /// <summary>
    /// Represents a JSON object for a collection of results
    /// returned from a Blizzard API search.
    /// </summary>
    public class BlizzardSearchResult
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

        /// <summary>
        /// A wrapper class for a list of search results.
        /// </summary>
        public class Result
        {
            [JsonPropertyName("data")]
            public Data Data { get; set; }
        }

        /// <summary>
        /// A data item in a search result.
        /// The key here is 'name' is class Name, to
        /// get the localization property out.
        /// </summary>
        public class Data
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("name")]
            public Name Name { get; set; }
        }

        /// <summary>
        /// The localized name of the search result.
        /// </summary>
        public class Name
        {
            public string en_US { get; set; }
        }
    }
}
