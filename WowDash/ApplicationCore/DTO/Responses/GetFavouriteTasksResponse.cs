using System;
using System.Collections.Generic;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class GetFavouriteTasksResponse
    {
        public Guid PlayerId { get; set; }
        public ICollection<TaskResponse> Tasks { get; set; }

        public GetFavouriteTasksResponse() { }

        public GetFavouriteTasksResponse(Guid playerId, ICollection<TaskResponse> tasks)
        {
            PlayerId = playerId;
            Tasks = tasks;
        }
    }
}
