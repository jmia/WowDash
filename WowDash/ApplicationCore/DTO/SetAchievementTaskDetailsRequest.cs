using System;
using System.ComponentModel.DataAnnotations;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash
{
    public class SetAchievementTaskDetailsRequest
    {
        [Required]
        public Guid TaskId { get; set; }
        public string Description { get; set; }
        [Required]
        public Priority Priority { get; set; }

        public SetAchievementTaskDetailsRequest(Guid taskId, string description, Priority priority)
        {
            TaskId = taskId;
            Description = description;
            Priority = priority;
        }
    }
}