namespace WowDash.ApplicationCore.DTO.Common
{
    /// <summary>
    /// Wow. This name is really terrible.
    /// </summary>
    public class FilterListSourceResponse
    {
        public int? GameId { get; set; }
        public string Name { get; set; }

        public FilterListSourceResponse() { }
        public FilterListSourceResponse(int? gameId, string name)
        {
            GameId = gameId;
            Name = name;
        }
    }
}
