using System;
using System.Collections.Generic;

namespace wow_dashboard.Models
{
    /// <summary>
    /// Represents a user of the application.
    /// </summary>
    public class User
    {
        public Guid Id { get; set; }
        public string GoogleId { get; set; }
        public string BlizzardId { get; set; }
        public ICollection<Character> Characters { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public string DisplayName { get; set; }
        public TaskType DefaultTaskType { get; set; }
        public string DefaultRealm { get; set; }
    }
}
