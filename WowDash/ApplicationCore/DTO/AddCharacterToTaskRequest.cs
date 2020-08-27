using System;

namespace WowDash.ApplicationCore.DTO
{
    public class AddCharacterToTaskRequest
    {
        public Guid CharacterId { get; }
        public Guid TaskId { get; }

        public AddCharacterToTaskRequest(Guid characterId, Guid taskId)
        {
            CharacterId = characterId;
            TaskId = taskId;
        }
    }
}
