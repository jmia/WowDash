using System;
using System.ComponentModel.DataAnnotations;

namespace WowDash.ApplicationCore.DTO
{
    public class SetTaskNotesRequest
    {
        [Required]
        public Guid TaskId { get; set; }
        public string Notes { get; set; }

        public SetTaskNotesRequest(Guid taskId, string notes)
        {
            TaskId = taskId;
            Notes = notes;
        }
    }
}