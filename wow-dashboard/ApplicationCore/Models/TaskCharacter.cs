using System;

namespace wow_dashboard.ApplicationCore.Models
{
    /// <summary>
    /// This is a joining/bridging class for tasks & characters.
    /// </summary>
    public class TaskCharacter
    {
        public Guid TaskId { get; set; }
        public Task Task { get; set; }
        public Guid CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
