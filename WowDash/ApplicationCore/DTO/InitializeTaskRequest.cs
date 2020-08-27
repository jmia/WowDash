using System;
using WowDash.ApplicationCore.Entities;

namespace WowDash.ApplicationCore.DTO
{
    public class InitializeTaskRequest
    {
        public Guid PlayerId { get; set; }
        public TaskType TaskType { get; set; }    // How to represent an enum in a DTO?

        public InitializeTaskRequest(Guid playerId, TaskType taskType = TaskType.General)
        {
            PlayerId = playerId;
            TaskType = taskType;
        }
    }
}