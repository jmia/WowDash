using System;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash
{
    public class SetGeneralTaskDetailsRequest
    {
        public Guid TaskId { get; }
        public string Description { get; }
        public RefreshFrequency RefreshFrequency { get; }
        public Priority Priority { get; }

        public SetGeneralTaskDetailsRequest(Guid taskId, string description, RefreshFrequency refreshFrequency,
            Priority priority)
        {
            TaskId = taskId;
            Description = description;
            RefreshFrequency = refreshFrequency;
            Priority = priority;
        }
    }
}