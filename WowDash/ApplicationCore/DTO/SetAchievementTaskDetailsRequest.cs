using System;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash
{
    public class SetAchievementTaskDetailsRequest
    {
        public Guid TaskId { get; }
        public string Description { get; }
        public Priority Priority { get; }

        public SetAchievementTaskDetailsRequest(Guid taskId, string description, Priority priority)
        {
            TaskId = taskId;
            Description = description;
            Priority = priority;
        }
    }
}