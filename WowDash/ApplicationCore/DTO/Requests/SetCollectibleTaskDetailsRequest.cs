using System;
using System.ComponentModel.DataAnnotations;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO.Requests
{
    public class SetCollectibleTaskDetailsRequest
    {
        [Required]
        public Guid TaskId { get; set; }
        /// <summary>
        /// A generated description of the task.
        /// </summary>
        public string Description { get; set; }
        [Required]
        public RefreshFrequency RefreshFrequency { get; set; }
        [Required]
        public Priority Priority { get; set; }

        public SetCollectibleTaskDetailsRequest() { }

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
