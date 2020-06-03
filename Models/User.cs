using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wow_dashboard.Models
{
    public class User
    {
        public int Id { get; set; }
        public string GoogleId { get; set; }
        public string BlizzardId { get; set; }
        public string DisplayName { get; set; }

        // TODO: Add foreign keys
        // - default character id
        // - default realm id
        // - default task type id
    }
}
