using System;

namespace WowDash.ApplicationCore.DTO
{
    public class SetAttemptIncompleteRequest
    {
        public Guid CharacterId { get; }
        public Guid TaskId { get; }

        public SetAttemptIncompleteRequest(Guid characterId, Guid taskId)
        {
            CharacterId = characterId;
            TaskId = taskId;
        }
    }
}
