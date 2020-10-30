using System;
using System.Collections.Generic;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.Entities
{
    /// <summary>
    /// Represents a user of the application.
    /// </summary>
    public class Player
    {
        public Guid Id { get; set; }
        public string IdentityUserId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string GoogleId { get; set; }
        public string BlizzardId { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public TaskType? DefaultTaskType { get; set; }
        public string DefaultRealm { get; set; }

        public Player()
        {
            Characters = new List<Character>();
            Tasks = new List<Task>();
        }
    }
}
