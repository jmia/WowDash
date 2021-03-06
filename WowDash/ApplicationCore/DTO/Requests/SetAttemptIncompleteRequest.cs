﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WowDash.ApplicationCore.DTO.Requests
{
    public class SetAttemptIncompleteRequest
    {
        [Required]
        public Guid CharacterId { get; set; }
        [Required]
        public Guid TaskId { get; set; }

        public SetAttemptIncompleteRequest() { }

        public SetAttemptIncompleteRequest(Guid characterId, Guid taskId)
        {
            CharacterId = characterId;
            TaskId = taskId;
        }
    }
}
