using System;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class GetPlayerProfileResponse
    {
        public Guid PlayerId { get; set; }
        public string DisplayName { get; set; }
        public TaskType? DefaultTaskType { get; set; }
        public string DefaultRealm { get; set; }

        public GetPlayerProfileResponse() { }

        public GetPlayerProfileResponse(Guid playerId, string displayName, TaskType? defaultTaskType, string defaultRealm)
        {
            PlayerId = playerId;
            DisplayName = displayName;
            DefaultTaskType = defaultTaskType;
            DefaultRealm = defaultRealm;
        }
    }
}
