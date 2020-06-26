using System;

namespace wow_dashboard.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string GoogleId { get; set; }
        public string BlizzardId { get; set; }
        public string DisplayName { get; set; }

        // TODO: Add foreign keys
        // - default character id
        // - default realm id
        // - default task type id
    }
}
