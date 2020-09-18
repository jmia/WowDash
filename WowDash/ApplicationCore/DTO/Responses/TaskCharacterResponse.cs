using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class TaskCharacterResponse
    {
        public Guid CharacterId { get; set; }
        public Guid TaskId { get; set; }
        public bool IsActive { get; set; }

        public TaskCharacterResponse() { }

        public TaskCharacterResponse(Guid characterId, Guid taskId, bool isActive)
        {
            CharacterId = characterId;
            TaskId = taskId;
            IsActive = isActive;
        }
    }
}
