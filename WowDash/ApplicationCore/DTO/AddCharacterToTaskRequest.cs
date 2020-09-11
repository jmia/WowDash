using System;
using System.ComponentModel.DataAnnotations;

namespace WowDash.ApplicationCore.DTO
{
    public class AddCharacterToTaskRequest
    {
        [Required]
        public Guid CharacterId { get; set; }
        [Required]
        public Guid TaskId { get; set; }

        public AddCharacterToTaskRequest(Guid characterId, Guid taskId)
        {
            CharacterId = characterId;
            TaskId = taskId;
        }
    }
}
