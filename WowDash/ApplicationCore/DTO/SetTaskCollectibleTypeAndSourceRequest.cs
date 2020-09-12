﻿using System;
using System.ComponentModel.DataAnnotations;
using static WowDash.ApplicationCore.Common.Enums;

namespace WowDash.ApplicationCore.DTO
{
    public class SetTaskCollectibleTypeAndSourceRequest
    {
        [Required]
        public Guid TaskId { get; set; }
        [Required]
        public CollectibleType CollectibleType { get; set; }
        [Required]
        public Source Source { get; set; }

        public SetTaskCollectibleTypeAndSourceRequest(Guid taskId, CollectibleType collectibleType, Source source)
        {
            TaskId = taskId;
            CollectibleType = collectibleType;
            Source = source;
        }
    }
}