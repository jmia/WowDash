using System;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO
{
    public class InitializeTaskRequest
    {
        public Guid PlayerId { get; set; }
        public TaskType TaskType { get; set; }

        public InitializeTaskRequest(Guid playerId, TaskType taskType)
        {
            PlayerId = playerId;
            TaskType = taskType;
        }
    }
}