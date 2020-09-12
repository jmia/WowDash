using System;
using System.ComponentModel.DataAnnotations;

namespace WowDash.ApplicationCore.DTO
{
    public class DeleteTaskRequest
    {
        [Required]
        public Guid TaskId { get; set; }

        public DeleteTaskRequest() { }

        public DeleteTaskRequest(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
