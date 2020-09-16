using System;
using System.ComponentModel.DataAnnotations;

namespace WowDash.ApplicationCore.DTO.Requests
{
    public class AddTaskToFavouritesRequest
    {
        [Required]
        public Guid TaskId { get; set; }

        public AddTaskToFavouritesRequest() { }

        public AddTaskToFavouritesRequest(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
