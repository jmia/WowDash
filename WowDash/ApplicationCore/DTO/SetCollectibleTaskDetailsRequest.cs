using System;
using System.ComponentModel.DataAnnotations;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO
{
    public class SetCollectibleTaskDetailsRequest
    {
        [Required]
        public Guid TaskId { get; }
        public string Description { get; }
        [Required]
        public RefreshFrequency RefreshFrequency { get; }
        [Required]
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
