using System;

namespace WowDash.ApplicationCore.Entities
{
    /// <summary>
    /// This is a joining/bridging class for tasks and characters.
    /// </summary>
    public class TaskCharacter
    {
        public Guid CharacterId { get; }
        public virtual Character Character { get; set; }
        public Guid TaskId { get; }
        public virtual Task Task { get; set; }
        public bool IsActive { get; set; }

        public TaskCharacter(Guid characterId, Guid taskId)
        {
            CharacterId = characterId;
            TaskId = taskId;
            IsActive = true;
        }
    }
}
