using System;
using System.ComponentModel.DataAnnotations;

namespace WowDash.ApplicationCore.DTO.Requests
{
    public class SetTaskNotesRequest
    {
        [Required]
        public Guid TaskId { get; set; }
        /// <summary>
        /// Extra notes related to the task.
        /// </summary>
        public string Notes { get; set; }

        public SetTaskNotesRequest() { }

        public SetTaskNotesRequest(Guid taskId, string notes)
        {
            TaskId = taskId;
            Notes = notes;
        }
    }
}