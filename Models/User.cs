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
        public ICollection<Character> Characters { get; set; }  // TODO - Set in OnModelCreating
        public ICollection<Task> Tasks { get; set; }    // TODO - Set in OnModelCreating
        public string DisplayName { get; set; }
        public TaskType DefaultTaskType { get; set; }   // TODO - Set in OnModelCreating
        public Character DefaultCharacter { get; set; }
        public Guid DefaultCharacterId { get; set; }    // TODO - Set in OnModelCreating
        public string DefaultRealm { get; set; }
    }
}
