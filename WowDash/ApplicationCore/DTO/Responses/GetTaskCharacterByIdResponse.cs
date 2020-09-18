using System;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class GetTaskCharacterByIdResponse
    {
        public Guid CharacterId { get; set; }
        public Guid TaskId { get; set;  }
        public bool IsActive { get; set; }

        public GetTaskCharacterByIdResponse() { }

        public GetTaskCharacterByIdResponse(Guid characterId, Guid taskId, bool isActive)
        {
            CharacterId = characterId;
            TaskId = taskId;
            IsActive = isActive;
        }
    }
}
