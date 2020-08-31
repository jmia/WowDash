using System;

namespace WowDash.ApplicationCore.DTO
{
    public class RemoveCharacterFromTaskRequest
    {
        public Guid CharacterId { get; }
        public Guid TaskId { get; }

        public RemoveCharacterFromTaskRequest(Guid characterId, Guid taskId)
        {
            CharacterId = characterId;
            TaskId = taskId;
        }
    }
}
