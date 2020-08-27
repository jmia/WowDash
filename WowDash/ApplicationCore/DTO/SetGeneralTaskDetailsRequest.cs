using System;

namespace WowDash
{
    public class SetGeneralTaskDetailsRequest
    {
        public Guid TaskId { get; set; }
        public string Description { get; set; }

        public SetGeneralTaskDetailsRequest(Guid taskId, string description)
        {
            Description = description;
            TaskId = taskId;
        }
    }
}