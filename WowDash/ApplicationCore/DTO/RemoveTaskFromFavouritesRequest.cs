using System;

namespace WowDash.ApplicationCore.DTO
{
    public class RemoveTaskFromFavouritesRequest
    {
        public Guid TaskId { get; }

        public RemoveTaskFromFavouritesRequest(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
