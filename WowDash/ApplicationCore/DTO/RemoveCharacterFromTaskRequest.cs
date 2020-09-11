using System;
using System.ComponentModel.DataAnnotations;

namespace WowDash.ApplicationCore.DTO
{
    public class RemoveCharacterFromTaskRequest
    {
        [Required]
        public Guid CharacterId { get; set; }
        [Required]
        public Guid TaskId { get; set; }

        public RemoveCharacterFromTaskRequest(Guid characterId, Guid taskId)
        {
            CharacterId = characterId;
            TaskId = taskId;
        }
    }
}
