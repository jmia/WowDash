using System;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO
{
    public class SetCollectibleTaskDetailsRequest
    {
        public Guid TaskId { get; }
        public string Description { get; }
        public RefreshFrequency RefreshFrequency { get; }
        public Priority Priority { get; }

        public SetCollectibleTaskDetailsRequest(Guid taskId, string description, RefreshFrequency refreshFrequency,
            Priority priority)
        {
            TaskId = taskId;
            Description = description;
            RefreshFrequency = refreshFrequency;
            Priority = priority;
        }
    }
}
