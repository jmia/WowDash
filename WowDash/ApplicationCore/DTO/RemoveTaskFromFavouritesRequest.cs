using System;

namespace WowDash.ApplicationCore.DTO
{
    public class RemoveTaskFromFavouritesRequest
    {
        public Guid TaskId { get; set; }

        public RemoveTaskFromFavouritesRequest(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
