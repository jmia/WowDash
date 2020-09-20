using System;
using System.Collections.Generic;

namespace WowDash.ApplicationCore.DTO.Responses
{
    public class GetTasksResponse
    {
        public Guid PlayerId { get; set; }
        public ICollection<TaskResponse> Tasks { get; set; }

        public GetTasksResponse() { }

        public GetTasksResponse(Guid playerId, ICollection<TaskResponse> tasks)
        {
            PlayerId = playerId;
            Tasks = tasks;
        }
    }
}
