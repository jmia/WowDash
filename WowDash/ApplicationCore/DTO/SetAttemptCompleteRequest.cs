using System;

namespace WowDash.ApplicationCore.DTO
{
    public class SetAttemptCompleteRequest
    {
        public Guid CharacterId { get; }
        public Guid TaskId { get; }

        public SetAttemptCompleteRequest(Guid characterId, Guid taskId)
        {
            CharacterId = characterId;
            TaskId = taskId;
        }
    }
}