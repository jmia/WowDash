﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WowDash.ApplicationCore.DTO.Requests
{
    public class RemoveTaskFromFavouritesRequest
    {
        [Required]
        public Guid TaskId { get; set; }

        public RemoveTaskFromFavouritesRequest() { }

        public RemoveTaskFromFavouritesRequest(Guid taskId)
        {
            TaskId = taskId;
        }
    }
}
