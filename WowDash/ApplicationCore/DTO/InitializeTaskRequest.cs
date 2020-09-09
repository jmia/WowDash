using System;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO
{
    public class InitializeTaskRequest
    {
        public Guid PlayerId { get; }
        public TaskType TaskType { get; }

        public InitializeTaskRequest(Guid playerId, TaskType taskType)
        {
            PlayerId = playerId;
            TaskType = taskType;
        }
    }
}