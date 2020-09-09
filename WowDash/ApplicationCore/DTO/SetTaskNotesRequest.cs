using System;

namespace WowDash.ApplicationCore.DTO
{
    public class SetTaskNotesRequest
    {
        public Guid TaskId { get; }
        public string Notes { get; }

        public SetTaskNotesRequest(Guid taskId, string notes)
        {
            TaskId = taskId;
            Notes = notes;
        }
    }
}