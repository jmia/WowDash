using System;

namespace WowDash.ApplicationCore.DTO
{
    public class DeleteTaskRequest
    {
        public Guid TaskId { get; }

        public DeleteTaskRequest(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
