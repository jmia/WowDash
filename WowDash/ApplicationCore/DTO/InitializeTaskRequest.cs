using System;
using System.ComponentModel.DataAnnotations;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO
{
    public class InitializeTaskRequest
    {
        [Required]
        public Guid PlayerId { get; set;  }
        [Required]
        public TaskType TaskType { get; set; }

        public InitializeTaskRequest() { }

        public InitializeTaskRequest(Guid playerId, TaskType taskType)
        {
            PlayerId = playerId;
            TaskType = taskType;
        }
    }
}