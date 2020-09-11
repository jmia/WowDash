using System;
using System.ComponentModel.DataAnnotations;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash
{
    public class SetGeneralTaskDetailsRequest
    {
        [Required]
        public Guid TaskId { get; set; }
        public string Description { get; set; }
        [Required]
        public RefreshFrequency RefreshFrequency { get; set; }
        [Required]
        public Priority Priority { get; set; }

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