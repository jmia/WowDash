using System;
using System.ComponentModel.DataAnnotations;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash
{
    public class SetAchievementTaskDetailsRequest
    {
        [Required]
        public Guid TaskId { get; set; }
        /// <summary>
        /// The name of the achievement.
        /// </summary>
        public string Description { get; set; }
        [Required]
        public Priority Priority { get; set; }

        public SetAchievementTaskDetailsRequest() { }

        public SetAchievementTaskDetailsRequest(Guid taskId, string description, Priority priority)
        {
            TaskId = taskId;
            Description = description;
            Priority = priority;
        }
    }
}