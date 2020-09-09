using System;

namespace WowDash.ApplicationCore.DTO
{
    public class AddTaskToFavouritesRequest
    {
        public Guid TaskId { get; set; }

        public AddTaskToFavouritesRequest(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
