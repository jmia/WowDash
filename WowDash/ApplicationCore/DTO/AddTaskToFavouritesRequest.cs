using System;
using System.ComponentModel.DataAnnotations;

namespace WowDash.ApplicationCore.DTO
{
    public class AddTaskToFavouritesRequest
    {
        [Required]
        public Guid TaskId { get; set; }

        public AddTaskToFavouritesRequest(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
